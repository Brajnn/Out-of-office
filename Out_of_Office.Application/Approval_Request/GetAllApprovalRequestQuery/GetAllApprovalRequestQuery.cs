using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Approval_Request.GetAllApprovalRequestQuery
{
    public class GetAllApprovalRequestQuery:IRequest<IEnumerable<ApprovalRequestDto>>
    {
    }
}
