using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Extensions;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.Shared.RequestFeatures;

[Authorize]
//[TypeFilter(typeof(CheckTokenExpirationAttribute))]
public class EmployeeController : Controller
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [Authorize(Roles = "HR Manager, Project Manager, Administrator")]
    public async Task<IActionResult> Index([FromQuery] EmployeeParameters employeeParameters)
    {
        if (employeeParameters.PageSize == 0 && employeeParameters.CurrentPage == 0)
        {
            employeeParameters.CurrentPage = 1;
            employeeParameters.PageSize = 10;
        }
        var employeeIndexVM = await employeeService.GetAllEmployeesAsync(employeeParameters);
        employeeIndexVM.EmployeeParameters = employeeParameters;
        return View(employeeIndexVM);
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Create([FromBody] CreateEmployeeVM employee)
    {
        if (!ModelState.IsValid || employee == null)
        {
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        employeeService.CreateEmployeeAsync(employee);
        return Json(new { success = true });
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Edit(Guid id)
    {
        // var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
        // if (employee == null)
        // {
        //     return NotFound();
        // }
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "HR Manager, Administrator")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit()
    {
        // if (id != employee.Id)
        // {
        //     return NotFound();
        // }

        // if (ModelState.IsValid)
        // {
        //     _context.Update(employee);
        //     _context.SaveChanges();
        //     return RedirectToAction(nameof(Index));
        // }
        return View();
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Deactivate(Guid id)
    {
        // var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
        // if (employee == null)
        // {
        //     return NotFound();
        // }
        // employee.StatusId = /* Deactivated status ID */;
        // _context.Update(employee);
        // _context.SaveChanges();
        // return RedirectToAction(nameof(Index));
        return View();
    }

    [Authorize(Roles = "Project Manager, Administrator")]
    public IActionResult Details(Guid id)
    {
        // var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
        // if (employee == null)
        // {
        //     return NotFound();
        // }
        // return View(employee);
        return View();
    }

    [Authorize(Roles = "Project Manager, Administrator")]
    public IActionResult AssignToProject(Guid id)
    {
        // var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
        // if (employee == null)
        // {
        //     return NotFound();
        // }
        // // Logic to assign the employee to a project
        // return View(employee);
        return View();
    }
}
