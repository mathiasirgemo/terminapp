using Microsoft.EntityFrameworkCore;
using Terminapp.Models;

namespace Terminapp.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<VacationRequest>()
                .HasKey(v => v.VacationId);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => s.ScheduleId);

            modelBuilder.Entity<ScheduleEntry>()
                .HasKey(se => se.Id);

            // Definer relasjonen mellom Employee og VacationRequest
            modelBuilder.Entity<VacationRequest>()
                .HasOne(v => v.Employee)
                .WithMany(e => e.VacationRequests)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Slett forespørselen hvis ansatte slettes
        }
    }
}