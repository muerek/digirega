using Blazored.Modal;
using Blazored.Modal.Services;
using DigiRega.Client.Services;
using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Shared.Errors;
using DigiRega.Shared.Dto.Club;
using DigiRega.Shared.Dto.Manager;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Shared.Managers
{
    /// <summary>
    /// Shows a list with all managers including options to edit, remove and add new ones.
    /// </summary>
    public partial class ManagersList
    {
        [CascadingParameter]
        IModalService ModalService { get; set; } = null!;

        [CascadingParameter]
        ErrorHandler ErrorHandler { get; set; } = null!;

        [Inject]
        public IClubService ClubService { get; set; } = null!;

        [Inject]
        public IManagerService ManagerService { get; set; } = null!;

        private IList<GetClubDto>? clubs = null;
        private IList<GetManagerDto>? managers = null;

        protected async override Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task ShowAddManagerModal()
        {
            var modal = ModalService.Show<AddManagerModal>("Obmensch hinzufügen");

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadDataAsync();
            }
        }

        private async Task ShowEditManagerModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditManagerModal.Id), id);
            var modal = ModalService.Show<EditManagerModal>("Obmensch bearbeiten", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadDataAsync();
            }
        }

        private async Task ShowDeleteManagerModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(DeleteManagerModal.Id), id);
            var modal = ModalService.Show<DeleteManagerModal>("Obmensch löschen", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadDataAsync();
            }
        }

        private async Task ShowDeleteClubModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(DeleteClubModal.Id), id);
            var modal = ModalService.Show<DeleteClubModal>("Verein löschen", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadDataAsync();
            }
        }

        /// <summary>
        /// Loads all data required for this view. Use for initializing and refreshing lateron.
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataAsync()
        {
            try
            {
                clubs = await ClubService.GetClubsAsync();
                managers = await ManagerService.GetManagersAsync();
            }
            catch (ServiceException e)
            {
                await ErrorHandler.HandleAsync(e);
            }
        }
    }
}
