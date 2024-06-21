using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Approval_Request.Query.GetApprovalRequestByIdQuery
{
    public class GetApprovalRequestByIdQuery : IRequest<ApprovalRequestDto>
    {
        public int Id { get; }
        public GetApprovalRequestByIdQuery(int id)
        {
            Id= id;
        }
    }
}
