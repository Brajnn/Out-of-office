using MediatR;
using Microsoft.AspNetCore.Mvc;
using Out_of_Office.Application.Approval_Request.GetAllApprovalRequestQuery;

namespace Out_of_Office.Controllers
{
    public class ApprovalRequestController:Controller
    {
        private readonly IMediator _mediator;

        public ApprovalRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var approvalRequests = await _mediator.Send(new GetAllApprovalRequestQuery());
            return View(approvalRequests);
        }
    }
}
