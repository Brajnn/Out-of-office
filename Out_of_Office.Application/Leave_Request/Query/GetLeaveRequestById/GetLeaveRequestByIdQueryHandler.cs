using AutoMapper;
using MediatR;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Leave_Request.Query.GetLeaveRequestById
{
    public class GetLeaveRequestByIdQueryHandler:IRequestHandler<GetLeaveRequestByIdQuery,LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestByIdQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(request.Id);
            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"LeaveRequest with ID {request.Id} not found.");
            }

            var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);
            return leaveRequestDto;
        }
    }
}
