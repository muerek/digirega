using DigiRega.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IEmailingQueueService
    {
        /// <summary>
        /// Adds a confirmation message for sending an entry to the emailing queue.
        /// The message is sent to the entry's author.
        /// </summary>
        /// <param name="entry"><see cref="Entry"/> to be confirmed.</param>
        /// <param name="priority">Message priority for sending, higher values will be processed first.</param>
        /// <returns></returns>
        Task QueueConfirmationAsync(Entry entry, int priority = 0);

        /// <summary>
        /// Adds a message notifying the original author of a status update of their entry to the emailing queue.
        /// </summary>
        /// <param name="entry"><see cref="Entry"/> for which a status update should be sent.</param>
        /// <param name="priority">Message priority for sending, higher values will be processed first.</param>
        /// <returns></returns>
        Task QueueStatusUpdateAsync(Entry entry, int priority = 0);

        /// <summary>
        /// Adds an invitation message with access credentials for a manager to the emailing queue.
        /// By default behavior, invitation messages are treated with priority over other messages.
        /// </summary>
        /// <param name="manager"><see cref="Manager"/> to be invited.</param>
        /// <param name="priority">
        /// Message priority for sending, higher values will be processed first.
        /// Invitation messages default to a higher priority than other messages if no custom priorities are set.
        /// </param>
        /// <returns></returns>
        Task QueueInvitationAsync(Manager manager, int priority = 1);
    }
}
