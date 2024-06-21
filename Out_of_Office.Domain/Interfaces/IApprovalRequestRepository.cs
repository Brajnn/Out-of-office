using Out_of_Office.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Domain.Interfaces
{
    public interface IApprovalRequestRepository
    {
        Task<IEnumerable<ApprovalRequest>> GetAllApprovalRequestsAsync();
        Task<ApprovalRequest> GetApprovalRequestByIdAsync(int id);
        Task AddApprovalRequestAsync(ApprovalRequest approvalRequest);
        Task UpdateAsync(ApprovalRequest approvalRequest);
    }
}
