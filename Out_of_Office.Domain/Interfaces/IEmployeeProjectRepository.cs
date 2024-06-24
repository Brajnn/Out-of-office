using Out_of_Office.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Domain.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        Task AddEmployeeProjectAsync(EmployeeProject employeeProject);
        Task RemoveEmployeeProjectAsync(int employeeId, int projectId);
        Task<EmployeeProject> GetEmployeeProjectAsync(int employeeId, int projectId);
        Task<IEnumerable<EmployeeProject>> GetProjectsByEmployeeIdAsync(int employeeId);
    }
}
