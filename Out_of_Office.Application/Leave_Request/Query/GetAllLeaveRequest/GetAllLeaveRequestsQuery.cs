using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Leave_Request.Query.GetAllLeaveRequest
{
    public class GetAllLeaveRequestsQuery : IRequest<IEnumerable<LeaveRequestDto>>
    {
        public int UserId { get; set; } 
        public string UserRole { get; set; }
    }
}
