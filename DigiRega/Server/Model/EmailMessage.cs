using DigiRega.Shared.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents an email message queued for processing.
    /// </summary>
    public class EmailMessage
    {
        public int Id { get; set; } = 0;

        public EmailStatus Status { get; set; } = EmailStatus.NotSent;

        /// <summary>
        /// Time this message was enqueued.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Time this message was sent successfully. Null if message has not been sent.
        /// </summary>
        public DateTimeOffset? SentAt { get; set; } = null;

        /// <summary>
        /// Priority for processing. Set higher values to increase priority.
        /// </summary>
        public int Priority { get; set; } = 0;

        public EmailRecipient Recipient { get; set; } = new();

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
