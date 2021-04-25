using DigiRega.Shared.Dto.StaffMember;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Validation.StaffMember
{
    public class UpdateStaffMemberValidator : AbstractValidator<UpdateStaffMemberDto>
    {
        public UpdateStaffMemberValidator()
        {
            // Password may be null if it is not updated.
            RuleFor(s => s.Password)
                .MinimumLength(6).WithMessage("Passwort braucht mindestens 6 Zeichen.")
                .When(s => s.Password != null);

            RuleFor(s => s.Role)
                .NotEmpty().WithMessage("Rolle ist erforderlich");
        }
    }
}
