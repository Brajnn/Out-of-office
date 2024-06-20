using Microsoft.EntityFrameworkCore;
using Out_of_Office.Domain.Entities;
using Out_of_Office.Domain.Interfaces;
using Out_of_Office.Infrastructure.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Infrastructure.Repositories
{
    public class ApprovalRequestRepository:IApprovalRequestRepository
    {
        private readonly Out_of_OfficeDbContext _context;

        public ApprovalRequestRepository(Out_of_OfficeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalRequest>> GetAllApprovalRequestsAsync()
        {
            return await _context.ApprovalRequest
                .Include(ar => ar.Approver)
                .Include(ar => ar.LeaveRequest)
                .ToListAsync();
        }
    }
}
