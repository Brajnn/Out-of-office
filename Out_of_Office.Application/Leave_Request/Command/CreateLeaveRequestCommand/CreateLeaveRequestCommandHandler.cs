using MediatR;
using Out_of_Office.Domain.Entities;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Leave_Request.Command.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = new LeaveRequest
            {
                EmployeeID = request.EmployeeId,
                AbsenceReason = request.AbsenceReason,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Comment = request.Comment,
                Status = LeaveRequest.AbsenceStatus.New
            };

            await _leaveRequestRepository.AddAsync(leaveRequest);
            return Unit.Value;
        }
    }
}
