using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Extensions;
using OutOfOffice.MVC.Models.Project;
using OutOfOffice.MVC.Shared.RequestFeatures;
using OutOfOffice.Shared.RequestFeatures;

[Authorize]
public class ProjectController : Controller
{
    private readonly IProjectService projectService;

    public ProjectController(IProjectService projectService)
    {
        this.projectService = projectService;
    }

    [Authorize(Roles = "HR Manager, Project Manager, Administrator")]
    public async Task<IActionResult> Index([FromQuery] ProjectParameters projectParameters)
    {
        projectParameters.Id = null;
        if (projectParameters.PageSize == 0 && projectParameters.CurrentPage == 0)
        {
            projectParameters.CurrentPage = 1;
            projectParameters.PageSize = 10;
        }
        var ProjectIndexVM = await projectService.GetAllProjectsAsync(projectParameters);
        ProjectIndexVM.ProjectParameters = projectParameters;
        return View(ProjectIndexVM);
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Create([FromBody] CreateProjectVM Project)
    {
        if (!ModelState.IsValid || Project == null)
        {
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        projectService.CreateProjectAsync(Project);
        return Json(new { success = true });
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Edit(Guid id)
    {
        // var Project = _context.Projects.FirstOrDefault(e => e.Id == id);
        // if (Project == null)
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
        // if (id != Project.Id)
        // {
        //     return NotFound();
        // }

        // if (ModelState.IsValid)
        // {
        //     _context.Update(Project);
        //     _context.SaveChanges();
        //     return RedirectToAction(nameof(Index));
        // }
        return View();
    }

    [Authorize(Roles = "HR Manager, Administrator")]
    public IActionResult Deactivate(Guid id)
    {
        // var Project = _context.Projects.FirstOrDefault(e => e.Id == id);
        // if (Project == null)
        // {
        //     return NotFound();
        // }
        // Project.StatusId = /* Deactivated status ID */;
        // _context.Update(Project);
        // _context.SaveChanges();
        // return RedirectToAction(nameof(Index));
        return View();
    }

    [Authorize(Roles = "Project Manager, Administrator")]
    public IActionResult Details(Guid id)
    {
        // var Project = _context.Projects.FirstOrDefault(e => e.Id == id);
        // if (Project == null)
        // {
        //     return NotFound();
        // }
        // return View(Project);
        return View();
    }

    [Authorize(Roles = "Project Manager, Administrator")]
    public IActionResult AssignToProject(Guid id)
    {
        // var Project = _context.Projects.FirstOrDefault(e => e.Id == id);
        // if (Project == null)
        // {
        //     return NotFound();
        // }
        // // Logic to assign the Project to a project
        // return View(Project);
        return View();
    }
}
