using MediatR;
using Microsoft.AspNetCore.Mvc;
using Out_of_Office.Application.Project.Command.CreateProject;
using Out_of_Office.Application.Project.Command.UpdateProjectStatus;
using Out_of_Office.Application.Project.Query.GetAllProjectsQuery;
using Out_of_Office.Application.Project.Query.GetProjectById;
using Out_of_Office.Domain.Entities;
using Out_of_Office.Domain.Interfaces;

namespace Out_of_Office.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectController(IMediator mediator, IEmployeeRepository employeeRepository)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? searchProjectId, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "type_asc" ? "type_desc" : "type_asc";
            ViewBag.StartDateSortParm = sortOrder == "startdate_asc" ? "startdate_desc" : "startdate_asc";
            ViewBag.ManagerSortParm = sortOrder == "manager_asc" ? "manager_desc" : "manager_asc";

            var projects = await _mediator.Send(new GetAllProjectsQuery());

            if (searchProjectId.HasValue)
            {
                projects = projects.Where(p => p.ID == searchProjectId.Value).ToList();
                ViewData["SearchProjectId"] = searchProjectId.Value;
            }

            projects = sortOrder switch
            {
                "id_desc" => projects.OrderByDescending(p => p.ID).ToList(),
                "type_asc" => projects.OrderBy(p => p.ProjectType).ToList(),
                "type_desc" => projects.OrderByDescending(p => p.ProjectType).ToList(),
                "startdate_asc" => projects.OrderBy(p => p.StartDate).ToList(),
                "startdate_desc" => projects.OrderByDescending(p => p.StartDate).ToList(),
                "manager_asc" => projects.OrderBy(p => p.ProjectManagerFullName).ToList(),
                "manager_desc" => projects.OrderByDescending(p => p.ProjectManagerFullName).ToList(),
                _ => projects.OrderBy(p => p.ID).ToList(),
            };

            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();
            ViewBag.ProjectManagers = projectManagers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var projectManagers = await _employeeRepository.GetProjectManagersAsync();
            ViewBag.ProjectManagers = projectManagers;
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            var updateProjectStatusCommand = new UpdateProjectStatusCommand
            {
                ProjectId = id,
                Status = ProjectStatus.Active
            };

            await _mediator.Send(updateProjectStatusCommand);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(int id)
        {
            var updateProjectStatusCommand = new UpdateProjectStatusCommand
            {
                ProjectId = id,
                Status = ProjectStatus.Inactive
            };

            await _mediator.Send(updateProjectStatusCommand);
            return RedirectToAction(nameof(Index));
        }

    }
    
}
