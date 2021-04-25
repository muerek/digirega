using Blazored.Modal;
using Blazored.Modal.Services;
using DigiRega.Client.Services;
using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Shared.Errors;
using DigiRega.Shared.Dto.Race;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Shared.Races
{
    public partial class RacesList
    {
        [Inject]
        public IRaceService RaceService { get; set; } = null!;

        [CascadingParameter]
        IModalService ModalService { get; set; } = null!;

        [CascadingParameter]
        ErrorHandler ErrorHandler { get; set; } = null!;

        private IList<GetRaceDto>? races = null;

        protected async override Task OnInitializedAsync()
        {
            await LoadRacesAsync();
        }

        private async Task ShowAddRaceModal()
        {
            var modal = ModalService.Show<AddRaceModal>("Rennen hinzufügen");

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadRacesAsync();
            }
        }

        private async Task ShowDeleteRaceModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(DeleteRaceModal.Id), id);
            var modal = ModalService.Show<DeleteRaceModal>("Rennen löschen", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadRacesAsync();
            }
        }

        private async Task LoadRacesAsync()
        {
            try { races = await RaceService.GetRacesAsync(); }
            catch (ServiceException e) { await ErrorHandler.HandleAsync(e); }
        }
    }
}
