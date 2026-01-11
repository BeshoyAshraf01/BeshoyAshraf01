namespace Esam.Controllers;
#region

using DAL.Database;
using DAL.Entities;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVm;

#endregion

public class EmployeeController : Controller{
    private readonly ApplicationDbContext _context;

    // استخدام الـ Constructor لعمل Dependency Injection
    public EmployeeController
    (
        ApplicationDbContext context
    ){
        _context = context;
    }


    // GET: /Employee/Index
    public async Task<IActionResult> Index(){
        var employees = await _context.Employees.ToListAsync();
        var list = employees.Select(selector: employee => new GetEmployeeVm
                             {
                                 Id = employee.Id,
                                 Name = employee.Name,
                                 Salary = employee.Salary,
                                 CreatedOn = employee.CreatedOn,
                                 ImageName = employee.Image,
                             })
                            .ToList();
        return View(list);
    }

    public string Print(){
        return "Hello from EmployeeController";
    }

    // GET: /Employee/Details/5
    public async Task<IActionResult> GetById
    (
        int id
    ){
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound($"Employee with Id {id} not found.");

        return View(employee);
    }

    public IActionResult Create(){
        return View();
    }

    [HttpPost] // حماية الـ Action وتحديد نوع الطلب
    public async Task<IActionResult> SaveDataCreate
    (
        GetEmployeeVm employee
    ){
        var image = Upload.UploadFile("File", employee.Image);
        if (!ModelState.IsValid) return View(nameof(Create), employee);
        Employee newEmployee = new()
        {
            Name = employee.Name,
            Salary = employee.Salary,
            Image = image,
        };
        var checkIsUnique = await _context.Employees
                                          .AnyAsync(predicate: e => e.Name.Equals(newEmployee.Name, StringComparison.CurrentCultureIgnoreCase));
        if (checkIsUnique){
            ModelState.AddModelError("Name", "Employee name must be unique.");
            return View(nameof(Create), employee);
        }
        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit
    (
        int id
    ){
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditData
    (
        Employee employee
    ){
        Employee newEmployee = new()
        {
            Name = employee.Name,
            Salary = employee.Salary,
            Image = employee.Image, // Ensure Image is preserved or handled if not part of the form
        };

        if (!ModelState.IsValid) return View(nameof(Edit), employee);

        var existingEmployee = await _context.Employees.FindAsync(employee.Id);
        if (existingEmployee == null) return NotFound();

        // تحديث البيانات
        existingEmployee.Name = employee.Name;
        existingEmployee.Salary = employee.Salary;

        await _context.SaveChangesAsync(); // ضروري جداً لحفظ التعديلات
        return RedirectToAction(nameof(Index));
    }

    // إضافية: مسح الموظف
    public async Task<IActionResult> Delete
    (
        int id
    ){
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null){
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
