using DigiRega.Server.Data;
using DigiRega.Shared.Validation.StaffMember;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Validators
{
    public class UsernameValidator : IUsernameValidator
    {
        private readonly DigiRegaDbContext db;

        public UsernameValidator(DigiRegaDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CheckUniqueAsync(string username)
        {
            return !(await db.Staff.AnyAsync(s => s.Username == username));
        }
    }
}
