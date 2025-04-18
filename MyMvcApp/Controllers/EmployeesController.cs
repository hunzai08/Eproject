using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NeondbContext _context;

        public EmployeesController(NeondbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.Employees.Include(e => e.Department).Include(e => e.Job).Include(e => e.Manager);
            return View(await neondbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId");
            ViewData["ManagerId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Employees/Create
        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Email,PhoneNumber,HireDate,JobId,Salary,CommissionPct,ManagerId,DepartmentId")] Employee employee)
        {
            // Validate if the model state is valid (including the EmployeeId input from the user)
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the new employee to the context
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    // Redirect to the list of employees after the employee is added successfully
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception (optional, but can be helpful for debugging)
                    // Add some error handling logic here (optional)
                    ModelState.AddModelError("", "There was an error saving the employee.");
                }
            }

            // In case of an invalid model, re-populate the dropdowns for JobId, ManagerId, and DepartmentId
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            ViewData["ManagerId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", employee.ManagerId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);

            // Return the view again with the invalid model so the user can correct it
            return View(employee);
        }


        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Email,PhoneNumber,HireDate,JobId,Salary,CommissionPct,ManagerId,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            ViewData["ManagerId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", employee.ManagerId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
