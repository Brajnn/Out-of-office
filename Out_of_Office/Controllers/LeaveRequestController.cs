using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Out_of_Office.Application.Leave_Request.Query;
using Out_of_Office.Application.Leave_Request.Query.GetAllLeaveRequest;
using Out_of_Office.Application.Leave_Request.Query.GetLeaveRequestById;
using Out_of_Office.Domain.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Out_of_Office.Application.Leave_Request.Command.CreateLeaveRequestCommand;
using Out_of_Office.Application.Leave_Request.Command.UpdateLeaveRequestStatus;

namespace Out_of_Office.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LeaveRequestController> _logger;

        public LeaveRequestController(IMediator mediator, ILogger<LeaveRequestController> logger, IUserRepository userRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? searchRequestId, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.EmployeeSortParm = sortOrder == "employee_asc" ? "employee_desc" : "employee_asc";
            ViewBag.StartDateSortParm = sortOrder == "startdate_asc" ? "startdate_desc" : "startdate_asc";

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (!int.TryParse(userIdString, out var userId))
            {
                _logger.LogError("Unable to parse UserId from NameIdentifier claim. Value: {UserIdString}", userIdString);
                return Unauthorized(); 
            }

            var user = await _userRepository.GetByIdAsync(userId);

            var employeeId = user.EmployeeId;
         
            var leaveRequests = await _mediator.Send(new GetAllLeaveRequestsQuery { UserId = employeeId, UserRole = userRole });


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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestCommand command)
        {
            if (ModelState.IsValid)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int userId;
                try
                {
                    userId = int.Parse(userIdString);
                }
                catch
                {
                    return Unauthorized();
                }

                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized();
                }

                command.EmployeeId = user.EmployeeId;

                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var command = new UpdateLeaveRequestStatusCommand
            {
                LeaveRequestID = id,
                Status = status
            };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
