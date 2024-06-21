using MediatR;
using Microsoft.AspNetCore.Mvc;
using Out_of_Office.Application.Project.Command.CreateProject;
using Out_of_Office.Application.Project.Query.GetAllProjectsQuery;
using Out_of_Office.Application.Project.Query.GetProjectById;

namespace Out_of_Office.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
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
        public IActionResult Create()
        {
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
            return View(command);
        }
    }
    
}
