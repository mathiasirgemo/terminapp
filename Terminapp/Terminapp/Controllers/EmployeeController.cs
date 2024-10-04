using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Terminapp.Data;
using Terminapp.Models;

public class EmployeeController : Controller
{
    private readonly EmployeeDbContext _context;

    public EmployeeController(EmployeeDbContext context)
    {
        _context = context;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        // Henter alle ansatte fra databasen
        return View(await _context.Employees.ToListAsync());
    }

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Henter en ansatt basert på id, inkludert ferieforespørsler
        var employee = await _context.Employees
            .Include(e => e.VacationRequests)  // Inkluderer relasjonen til VacationRequests
            .FirstOrDefaultAsync(m => m.EmployeeId == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

// GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

// POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployeeName,EmployeeEmail,Employment")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        if (!ModelState.IsValid)
        {
            // Skriv ut feilene
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(employee);
        
    }


    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Henter ansatt basert på id
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,EmployeeEmail,Employment")] Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Oppdaterer ansatt i databasen
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeId))
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
        return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Henter ansatt basert på id
        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.EmployeeId == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Fjerner ansatt fra databasen
        var employee = await _context.Employees.FindAsync(id);
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Sjekker om en ansatt eksisterer
    private bool EmployeeExists(int id)
    {
        return _context.Employees.Any(e => e.EmployeeId == id);
    }
}
