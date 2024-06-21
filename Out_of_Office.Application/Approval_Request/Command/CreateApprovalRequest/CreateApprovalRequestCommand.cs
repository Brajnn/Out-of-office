using MediatR;
using Out_of_Office.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Approval_Request.Command.CreateApprovalRequest
{
    public class CreateApprovalRequestCommand : IRequest
    {
        public int ApproverID { get; set; }
        public int LeaveRequestID { get; set; }
        public ApprovalStatus Status { get; set; }
        public string Comment { get; set; }
    }
}
