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
    public class LeaveRequestRepository:ILeaveRequestRepository
    {
        private readonly Out_of_OfficeDbContext _context;

        public LeaveRequestRepository(Out_of_OfficeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequest
                .Include(lr => lr.Employee) 
                .ToListAsync();
        }
        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            return await _context.LeaveRequest
                .Include(lr => lr.Employee)
                .FirstOrDefaultAsync(lr => lr.ID == id);
        }
        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequest.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
