using DigiRega.Server.Data;
using DigiRega.Server.Services;
using DigiRega.Shared.Definitions;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
{
    /// <summary>
    /// Grabs queued messages from the database and sends them as emails.
    /// </summary>
    public class BackgroundEmailSender : BackgroundService
    {
        private readonly IServiceProvider services;
        private readonly ILogger<BackgroundEmailSender> logger;
        private readonly EmailingOptions options;

        public BackgroundEmailSender(IServiceProvider services, ILogger<BackgroundEmailSender> logger, IOptions<EmailingOptions> options)
        {
            this.services = services;
            this.logger = logger;
            this.options = options.Value;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Background email sender is starting.");
            await SendQueuedMessagesAsync(stoppingToken);
        }

        private async Task SendQueuedMessagesAsync(CancellationToken cancellation)
        {
            // DbContext is a scoped service. This is a hosted service which is treated like a singleton.
            // Injecting DbContext via constructor does not work due to lifetime mismatch.
            // Instead, we need to create a scope and then use a service locator to get a DbContext instance.
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DigiRegaDbContext>();

            while (!cancellation.IsCancellationRequested)
            {
                // Check for the next message in the queue.
                // We need to look for the highest priority first, then pick the oldest.
                var message = await db.EmailMessages
                    .Where(e => e.Status == EmailStatus.NotSent)
                    .OrderByDescending(e => e.Priority)
                    .ThenBy(e => e.CreatedAt)
                    .FirstOrDefaultAsync(cancellationToken: cancellation);

                // If there is nothing in the queue, wait for some time before checking again.
                if (message == null)
                {
                    logger.LogDebug("No messages to be sent, checking back later.");
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellation);
                    continue;
                }

                try
                {
                    logger.LogInformation("Found message {Id} to {Recipient}, now sending.", message.Id, message.Recipient.EmailAddress);
                    
                    // Put the actual email together.
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress(options.FromName, options.FromAddress));
                    email.To.Add(new MailboxAddress(message.Recipient.Name, message.Recipient.EmailAddress));
                    email.Subject = message.Subject;
                    email.Body = new TextPart("html") { Text = message.Body };

                    // Send the email using MailKit.
                    using var client = new SmtpClient();
                    await client.ConnectAsync(options.SmtpServer, options.SmtpPort, options.UseSsl, cancellation);
                    await client.AuthenticateAsync(options.User, options.Password, cancellation);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);

                    logger.LogDebug("Successfully sent message {Id}.", message.Id);

                    // Flag the message as sent and update the sent timestamp.
                    message.SentAt = DateTimeOffset.Now;
                    message.Status = EmailStatus.Sent;
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Failed to send message {Id}.", message.Id);
                    // If something went wrong, flag the message accordingly.
                    message.Status = EmailStatus.Failed;
                }

                // Update the message data in the queue.
                db.EmailMessages.Update(message);
                await db.SaveChangesAsync();
            }
        }
    }
}
