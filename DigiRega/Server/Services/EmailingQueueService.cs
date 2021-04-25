using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    // Note that this service does not derive from ServiceBase as most of its contents are not needed.
    public class EmailingQueueService : IEmailingQueueService
    {
        // Messages are enqueued by putting them in the database.
        // The sender service will pick them up there.
        // Storing them in the database makes the queue persistent across crashes and restarts.
        // TODO: Is there a better way than to abuse the database as a message bus?
        private readonly DigiRegaDbContext db;

        /// <summary>
        /// Information on the web hosting environment, used to locate static files.
        /// </summary>
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<EmailingQueueService> logger;

        /// <summary>
        /// App config.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Caches email templates so they do not need to be re-rendered every time.
        /// </summary>
        private static ConcurrentDictionary<string, Template> emailTemplates = new();

        public EmailingQueueService(DigiRegaDbContext db, IWebHostEnvironment webHostEnvironment,
            ILogger<EmailingQueueService> logger, IConfiguration configuration)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task QueueConfirmationAsync(Entry entry, int priority = 0)
        {
            logger.LogInformation("Queueing confirmation email for entry {Id}.", entry.Id);

            var email = new EmailMessage()
            {
                Recipient = new()
                {
                    Name = $"{entry.SentBy.FirstName} {entry.SentBy.LastName}",
                    EmailAddress = entry.SentBy.EmailAddress
                },
                Subject = GetSubject(entry),
                Body = await RenderConfirmationAsync(entry),
                Priority = priority
            };

            await QueueEmailAsync(email);
        }

        public async Task QueueInvitationAsync(Manager manager, int priority = 1)
        {
            logger.LogInformation("Queueing invitation email for manager {Id}.", manager.Id);

            var email = new EmailMessage()
            {
                Recipient = new()
                {
                    Name = $"{manager.FirstName} {manager.LastName}",
                    EmailAddress = manager.EmailAddress
                },
                Subject = "DigiRega-Zugangsdaten",
                Body = await RenderInvitationAsync(manager),
                Priority = priority
            };

            await QueueEmailAsync(email);
        }

        public Task QueueStatusUpdateAsync(Entry entry, int priority = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper method to wrap the enqueuement procedure for an email message.
        /// </summary>
        /// <param name="email">Email message to be enqueued.</param>
        /// <returns></returns>
        private async Task QueueEmailAsync(EmailMessage email)
        {
            db.EmailMessages.Add(email);
            await db.SaveChangesAsync();

            logger.LogInformation("Added email on \"{Subject}\" to {Recipient} to queue.", email.Subject, email.Recipient);
        }

        private async Task<string> RenderInvitationAsync(Manager manager)
        {
            return await RenderTemplateAsync("Invitation.sbnhtml", manager);
        }

        private async Task<string> RenderConfirmationAsync(Entry entry)
        {
            // The different entry types have to use separate templates each.
            var templateName = entry switch
            {
                CrewChange => "CrewChange.sbnhtml",
                LateEntry => "LateEntry.sbnhtml",
                Withdrawal => "Withdrawal.sbnhtml",
                _ => throw new ArgumentException("Unknown entry type.", nameof(entry))
            };
            return await RenderTemplateAsync(templateName, entry);
        }

        /// <summary>
        /// Renders the template with the given name with the given data.
        /// </summary>
        /// <param name="templateName">Filename of the template to be rendered. Must be placed in wwwroot/EmailTemplates.</param>
        /// <param name="data">Data to be rendered in the template.</param>
        /// <returns></returns>
        private async Task<string> RenderTemplateAsync(string templateName, object data)
        {
            // If the template has not been rendered yet, render it and cache it.
            if (!emailTemplates.TryGetValue(templateName, out Template? template))
            {
                logger.LogDebug("Parsing email template {TemplateName} for first use.", templateName);

                // Locate the template in the template folder.
                var path = Path.Combine(webHostEnvironment.WebRootPath, "EmailTemplates", templateName);
                var text = await System.IO.File.ReadAllTextAsync(path);
                template = Template.Parse(text);
                // We could check, but for now it is easier to just not care if adding to the cache worked or not.
                emailTemplates.TryAdd(templateName, template);
            }

            logger.LogDebug("Rendering email template {TemplateName}.", templateName);

            // Make the data accessible for the template.
            var scriptObject = new ScriptObject { ["data"] = data, ["login"] = configuration.GetValue<string>("LoginUrl") };
            var context = new TemplateContext();
            context.PushGlobal(scriptObject);

            return await template.RenderAsync(context);
        }

        private static string GetSubject(Entry entry) 
        {
            var entryTypeInfo = entry switch
            {
                CrewChange cc => $"Ummeldung Startnr. {cc.BowNumber}",
                LateEntry => "Nachmeldung",
                Withdrawal w => $"Abmeldung Startnr. {w.BowNumber}",
                _ => throw new ArgumentException("Unknown entry type.", nameof(entry))
            };

            return $"{entryTypeInfo} in Rennen {entry.Race.Number}";
        }
    }
}
