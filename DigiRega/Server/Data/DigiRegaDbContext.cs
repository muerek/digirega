using DigiRega.Server.Model;
using DigiRega.Server.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Data
{
    public class DigiRegaDbContext : DbContext
    {
        public DbSet<Club> Clubs => Set<Club>();

        public DbSet<Race> Races => Set<Race>();

        public DbSet<EmailMessage> EmailMessages => Set<EmailMessage>();

        // All subtypes of User are mapped into one table (TPH).
        public DbSet<User> Users => Set<User>();
        public DbSet<StaffMember> Staff => Set<StaffMember>();
        public DbSet<Manager> Managers => Set<Manager>();
        
        // All subtypes of Entry are mapped into one table (TPH).
        public DbSet<Entry> Entries => Set<Entry>();
        public DbSet<CrewChange> CrewChanges => Set<CrewChange>();
        public DbSet<Withdrawal> Withdrawals => Set<Withdrawal>();
        public DbSet<LateEntry> LateEntries => Set<LateEntry>();

        public DigiRegaDbContext(DbContextOptions<DigiRegaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CrewChange and Withdrawal both have a BowNumber property.
            // This can be mapped to the same column as they serve the same purpose.
            modelBuilder.Entity<CrewChange>()
                .Property(c => c.BowNumber)
                .HasColumnName("BowNumber");
            modelBuilder.Entity<Withdrawal>()
                .Property(w => w.BowNumber)
                .HasColumnName("BowNumber");

            modelBuilder.Entity<Entry>()
                .HasDiscriminator<string>("EntryType");

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Type");

            // Add intial admin user.
            modelBuilder.Entity<StaffMember>().HasData(GetDefaultUser());
        }

        /// <summary>
        /// Builds the admin user to be used as the initial app user.
        /// </summary>
        /// <returns></returns>
        private StaffMember GetDefaultUser()
        {
            // TODO: Username and password should be configurable.
            var (hash, salt) = HashingHelper.CreateHash("admin");
            return new StaffMember()
            {
                Id = 1,
                Username = "admin",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "Admin"
            };
        }
    }
}
