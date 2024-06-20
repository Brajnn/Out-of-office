using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Leave_Request.Query.GetLeaveRequestById
{
    public class GetLeaveRequestByIdQuery : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
