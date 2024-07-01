using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.Shared.RequestFeatures;

[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    // [Authorize(Roles = "HR, Project Manager, Administrator")]
    // public async Task<IActionResult> Index()
    // {
    //     EmployeeParameters employeeParameters = new EmployeeParameters();
    //     var employees = await employeeService.GetAllEmployeesAsync(employeeParameters);
    //     return View(employees);
    // }

    [Authorize(Roles = "HR, Project Manager, Administrator")]
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

    [Authorize(Roles = "HR, Administrator")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "HR, Administrator")]
    public IActionResult Create([FromBody] CreateEmployeeVM employee)
    {
        try
        {
            // var employee = _mapper.Map<CreateEmployeeVM
            if (ModelState.IsValid)
            {
                employeeService.CreateEmployeeAsync(employee);
                return Json(new { success = true });
            }
            // return View(employee);
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, errors = ex.Message });
        }

    }

    [Authorize(Roles = "HR, Administrator")]
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
    [Authorize(Roles = "HR, Administrator")]
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

    [Authorize(Roles = "HR")]
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

    [Authorize(Roles = "Project Manager")]
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

    [Authorize(Roles = "Project Manager")]
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
