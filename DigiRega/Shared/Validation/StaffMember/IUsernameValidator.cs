using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Validation.StaffMember
{
    public interface IUsernameValidator
    {
        Task<bool> CheckUniqueAsync(string username);
    }
}
