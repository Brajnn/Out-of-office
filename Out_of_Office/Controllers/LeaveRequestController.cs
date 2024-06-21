using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Out_of_Office.Application.Leave_Request.Query;
using Out_of_Office.Application.Leave_Request.Query.GetAllLeaveRequest;
using Out_of_Office.Application.Leave_Request.Query.GetLeaveRequestById;

namespace Out_of_Office.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? searchRequestId, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.EmployeeSortParm = sortOrder == "employee_asc" ? "employee_desc" : "employee_asc";
            ViewBag.StartDateSortParm = sortOrder == "startdate_asc" ? "startdate_desc" : "startdate_asc";

            var leaveRequests = await _mediator.Send(new GetAllLeaveRequestsQuery());

            if (searchRequestId.HasValue)
            {
                leaveRequests = leaveRequests.Where(lr => lr.ID == searchRequestId.Value).ToList();
                ViewData["SearchRequestId"] = searchRequestId.Value;
            }
            leaveRequests = sortOrder switch
            {
                "id_desc" => leaveRequests.OrderByDescending(lr => lr.ID).ToList(),
                "employee_asc" => leaveRequests.OrderBy(lr => lr.EmployeeFullName).ToList(),
                "employee_desc" => leaveRequests.OrderByDescending(lr => lr.EmployeeFullName).ToList(),
                "startdate_asc" => leaveRequests.OrderBy(lr => lr.StartDate).ToList(),
                "startdate_desc" => leaveRequests.OrderByDescending(lr => lr.StartDate).ToList(),
                _ => leaveRequests.OrderBy(lr => lr.ID).ToList(), 
            };

            return View(leaveRequests);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestByIdQuery { Id = id });
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }
    }
}
