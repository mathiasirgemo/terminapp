using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Terminapp.Data;
using Terminapp.Models;

namespace Terminapp.Controllers
{
    public class VacationRequestsController : Controller
    {
        private readonly EmployeeDbContext _context;

        public VacationRequestsController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: VacationRequests
        public async Task<IActionResult> Index()
        {
            var vacationRequests = await _context.VacationRequests.Include(v => v.Employee).ToListAsync();
            return View(vacationRequests);
        }

        // GET: VacationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VacationRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacationId,EmployeeId,StartDate,EndDate,Status,Comment")] VacationRequest vacationRequest)
        {
            if (ModelState.IsValid)
            {
                vacationRequest.Status = "Ventende"; // Setter initial status til "Ventende"
                _context.Add(vacationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacationRequest);
        }

        // GET: VacationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }
            return View(vacationRequest);
        }

        // POST: VacationRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacationId,EmployeeId,StartDate,EndDate,Status,Comment")] VacationRequest vacationRequest)
        {
            if (id != vacationRequest.VacationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationRequestExists(vacationRequest.VacationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vacationRequest);
        }

        // GET: VacationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests
                .FirstOrDefaultAsync(m => m.VacationId == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // POST: VacationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            _context.VacationRequests.Remove(vacationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: VacationRequests/Godkjenn/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Godkjenn(int id)
        {
            var request = await _context.VacationRequests.FindAsync(id);
            if (request != null)
            {
                request.Status = "Godkjent";
                _context.Update(request);
                await _context.SaveChangesAsync();

                // Send notification (implementer etter behov)
                // SendNotification(request.EmployeeId, "Din ferieforespørsel er godkjent.");
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: VacationRequests/Avvis/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Avvis(int id, string comment)
        {
            var request = await _context.VacationRequests.FindAsync(id);
            if (request != null)
            {
                request.Status = "Avvist";
                request.Comment = comment;
                _context.Update(request);
                await _context.SaveChangesAsync();

                // Send notification (implementer etter behov)
                // SendNotification(request.EmployeeId, "Din ferieforespørsel er avvist.");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VacationRequestExists(int id)
        {
            return _context.VacationRequests.Any(e => e.VacationId == id);
        }
    }
}
