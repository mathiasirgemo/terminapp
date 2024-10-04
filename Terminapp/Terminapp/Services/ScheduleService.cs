using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Terminapp.Data;
using Terminapp.Models;

namespace Terminapp.Services
{
    public class ScheduleService
    {
        private readonly EmployeeDbContext _context; // Database-konteksten

        public ScheduleService(EmployeeDbContext context)
        {
            _context = context;
        }

        public Schedule GenerateSchedule(DateTime startDate, DateTime endDate)
        {
            var employees = _context.Employees.Include(e => e.VacationRequests).ToList();
            var schedule = new Schedule
            {
                StartDate = startDate,
                EndDate = endDate,
                Status = "Generert", // Initial status for the schedule
                Entries = new List<ScheduleEntry>()
            };

            // Logikk for å fordele vakter basert på stillingsprosent og ferieforespørsel
            foreach (var employee in employees)
            {
                // Sjekk om ansatte har ferie i denne perioden
                bool hasVacation = employee.VacationRequests.Any(v => 
                    v.StartDate <= endDate && v.EndDate >= startDate && v.Status == "Godkjent");

                if (!hasVacation)
                {
                    // Fordel vakter basert på stillingsprosent
                    int numberOfShifts = (int)(employee.Employment / 100 * (endDate - startDate).Days);
                    for (int i = 0; i < numberOfShifts; i++)
                    {
                        // Legg til vakter her, for eksempel på forskjellige datoer
                        schedule.Entries.Add(new ScheduleEntry
                        {
                            EmployeeId = employee.EmployeeId,
                            Shift = "Dag", // Tilpass etter behov
                            Date = startDate.AddDays(i) // Dette kan endres
                        });
                    }
                }
            }

            // Lagre terminlisten i databasen
            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return schedule;
        }

        public Schedule GetScheduleById(int id)
        {
            return _context.Schedules.Include(s => s.Entries)
                .ThenInclude(e => e.EmployeeId) // Om du ønsker å inkludere ansattdata
                .FirstOrDefault(s => s.ScheduleId == id);
        }
        public List<Schedule> GetAllSchedules()
        {
            return _context.Schedules.Include(s => s.Entries).ToList();
        }

    }
}
