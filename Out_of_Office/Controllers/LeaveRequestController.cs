using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int? SearchRequestId)
        {
            var leaveRequests = await _mediator.Send(new GetAllLeaveRequestsQuery());

            if (SearchRequestId.HasValue)
            {
                leaveRequests = leaveRequests.Where(lr => lr.ID == SearchRequestId.Value).ToList();
                ViewData["SearchRequestId"] = SearchRequestId.Value;
            }

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
