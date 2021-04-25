using Blazored.Modal;
using Blazored.Modal.Services;
using DigiRega.Client.Services;
using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Shared.Errors;
using DigiRega.Shared.Dto.StaffMember;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Shared.Staff
{
    /// <summary>
    /// Shows a list with all staff members including options to edit, delete or add new ones.
    /// </summary>
    public partial class StaffList
    {
        [Inject]
        public IStaffService StaffService { get; set; } = null!;

        [CascadingParameter]
        IModalService ModalService { get; set; } = null!;

        [CascadingParameter]
        ErrorHandler ErrorHandler { get; set; } = null!;

        private IList<GetStaffMemberDto> staff { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadStaffAsync();
        }

        private async Task ShowEditStaffMemberModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditStaffModal.Id), id);
            var modal = ModalService.Show<EditStaffModal>("Mitarbeiter bearbeiten", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadStaffAsync();
            }
        }

        private async Task ShowDeleteStaffMemberModal(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(DeleteStaffModal.Id), id);
            var modal = ModalService.Show<DeleteStaffModal>("Mitarbeiter löschen", parameters);

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadStaffAsync();
            }
        }

        private async Task ShowAddStaffMemberModal()
        {
            var modal = ModalService.Show<AddStaffModal>("Mitarbeiter hinzufügen");

            // Refresh data to pick up changes, but not if modal was cancelled without doing anything.
            if (!(await modal.Result).Cancelled)
            {
                await LoadStaffAsync();
            }
        }

        /// <summary>
        /// Loads all data required for this view. Use for initializing and refreshing lateron.
        /// </summary>
        /// <returns></returns>
        private async Task LoadStaffAsync()
        {
            try { staff = await StaffService.GetStaffMembersAsync(); }
            catch (ServiceException e) { await ErrorHandler.HandleAsync(e); }
        }
    }
}
