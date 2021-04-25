using DigiRega.Shared.Dto.StaffMember;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigiRega.Shared.Validation.StaffMember
{
    public class AddStaffMemberValidator : AbstractValidator<AddStaffMemberDto>
    {
        private readonly IUsernameValidator usernameValidator;

        public AddStaffMemberValidator(IUsernameValidator usernameValidator)
        {
            this.usernameValidator = usernameValidator;

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("Passwort ist erforderlich.")
                .MinimumLength(6).WithMessage("Passwort braucht mindestens 6 Zeichen.");

            RuleFor(s => s.Username)
                .NotEmpty().WithMessage("Benutzername ist erforderlich.")
                .MustAsync(IsUniqueUsername).WithMessage("Benutzername bereits vergeben.");

            RuleFor(s => s.Role)
                .NotEmpty().WithMessage("Rolle ist erforderlich");
        }

        private async Task<bool> IsUniqueUsername(string username, CancellationToken cancellation)
        {
            return await usernameValidator.CheckUniqueAsync(username);
        }
    }
}
