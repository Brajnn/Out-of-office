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
    public class EmployeeProjectRepository: IEmployeeProjectRepository
    {
        private readonly Out_of_OfficeDbContext _context;

        public EmployeeProjectRepository(Out_of_OfficeDbContext context)
        {
            _context = context;
        }
        public async Task RemoveEmployeeProjectAsync(int employeeId, int projectId) 
        {
            var employeeProject = await _context.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
            if (employeeProject != null)
            {
                _context.EmployeeProjects.Remove(employeeProject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddEmployeeProjectAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Add(employeeProject);
            await _context.SaveChangesAsync();
        }
        public async Task<EmployeeProject> GetEmployeeProjectAsync(int employeeId, int projectId)
        {
            return await _context.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        }
        public async Task<IEnumerable<EmployeeProject>> GetProjectsByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeProjects
                .Where(ep => ep.EmployeeId == employeeId)
                .ToListAsync();
        }
    }
}
