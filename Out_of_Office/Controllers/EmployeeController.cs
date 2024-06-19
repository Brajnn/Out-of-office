using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Out_of_Office.Application.Employee;
using Out_of_Office.Application.Employee.Command.CreateEmployee;
using Out_of_Office.Application.Employee.Queries.GetAllEmployees;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Out_of_Office.Controllers
{
    public class EmployeeController :Controller
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.SubdivisionSortParm = sortOrder == "Subdivision" ? "subdivision_desc" : "Subdivision";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            ViewBag.OutOfOfficeBalanceSortParm = sortOrder == "OutOfOfficeBalance" ? "balance_desc" : "OutOfOfficeBalance";
            ViewBag.CurrentFilter = searchString;

            var employees = await _mediator.Send(new GetAllEmployeesQuery());

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            employees = sortOrder switch
            {
                "id_desc" => employees.OrderByDescending(e => e.Id).ToList(),
                "Name" => employees.OrderBy(e => e.FullName).ToList(),
                "name_desc" => employees.OrderByDescending(e => e.FullName).ToList(),
                "Subdivision" => employees.OrderBy(e => e.Subdivision).ToList(),
                "subdivision_desc" => employees.OrderByDescending(e => e.Subdivision).ToList(),
                "Position" => employees.OrderBy(e => e.Position).ToList(),
                "position_desc" => employees.OrderByDescending(e => e.Position).ToList(),
                "Status" => employees.OrderBy(e => e.Status).ToList(),
                "status_desc" => employees.OrderByDescending(e => e.Status).ToList(),
                "OutOfOfficeBalance" => employees.OrderBy(e => e.OutOfOfficeBalance).ToList(),
                "balance_desc" => employees.OrderByDescending(e => e.OutOfOfficeBalance).ToList(),
                _ => employees.OrderBy(e => e.Id).ToList(),
            };

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateEmployee");
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View("CreateEmployee", command);
        }

        
        //public async task<iactionresult> edit(int id)
        //{
        //    var employee = await _mediator.send(new getemployeebyidquery(id));
        //    if (employee == null)
        //    {
        //        return notfound();
        //    }
        //    return view(employee);
        //}

        
        //[httppost]
        //[validateantiforgerytoken]
        //public async task<iactionresult> edit(int id, employeedto employee)
        //{
        //    if (id != employee.id)
        //    {
        //        return badrequest();
        //    }

        //    if (modelstate.isvalid)
        //    {
        //        await _mediator.send(new updateemployeecommand(employee));
        //        return redirecttoaction(nameof(index));
        //    }
        //    return view(employee);
        //}
    }
}
