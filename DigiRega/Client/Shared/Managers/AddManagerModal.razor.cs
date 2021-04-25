using Blazored.Modal;
using DigiRega.Client.Services;
using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Shared.Errors;
using DigiRega.Shared.Dto.Club;
using DigiRega.Shared.Dto.Manager;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Shared.Managers
{
    public partial class AddManagerModal
    {
        [CascadingParameter]
        public BlazoredModalInstance Modal { get; set; } = null!;

        [CascadingParameter]
        public ErrorHandler ErrorHandler { get; set; } = null!;

        [Inject]
        public IClubService ClubService { get; set; } = null!;

        [Inject]
        public IManagerService ManagerService { get; set; } = null!;

        private AddManagerDto manager { get; set; } = new();
        private IList<GetClubDto>? clubs = null;

        /// <summary>
        /// Flag if a new club is to be created. This is an int because bool does not play nice with data binding.
        /// Set to 1 to create a new club, any other value to use an existing club.
        /// </summary>
        private int newClub = 0;
        private string newClubName = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                clubs = await ClubService.GetClubsAsync();
            }
            catch (ServiceException e)
            {
                await ErrorHandler.HandleAsync(e);
                await Modal.CancelAsync();
            }
        }

        /// <summary>
        /// Tries to submit the form data. If a new club is used, the club is created, then all form data is validated.
        /// Only valid data is posted to the server.
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task TrySubmit(EditContext editContext)
        {
            // Check if a new club has to be added before the manager data is submitted.
            if (newClub == 1)
            {
                // Cannot proceed if there is no name for the new club.
                if (string.IsNullOrWhiteSpace(newClubName))
                {
                    // TODO: Add validation message
                    return;
                }
                // Add the new club and update the manager.
                try
                {
                    var newClubId = await AddNewClubAsync(newClubName);
                    // Update the manager data so it will validate later.
                    manager.ClubId = newClubId;
                }
                catch (ServiceException e)
                {
                    await ErrorHandler.HandleAsync(e);
                    // Cannot add manager without club.
                    return;
                }
            }

            // Check if the manager data can be submitted.
            if (editContext.Validate())
            {
                try
                {
                    await ManagerService.AddManagerAsync(manager);
                }
                catch (ServiceException e) { await ErrorHandler.HandleAsync(e); }

                await Modal.CloseAsync();
            }
        }

        /// <summary>
        /// Adds a new club and refreshes the club list after that.
        /// </summary>
        /// <returns>ID of the newly added club.</returns>
        private async Task<int> AddNewClubAsync(string newClubName)
        {
            var newClub = new AddClubDto() { Name = newClubName };
            var clubId = await ClubService.AddClubAsync(newClub);
            clubs = await ClubService.GetClubsAsync();
            return clubId;
        }
    }
}
