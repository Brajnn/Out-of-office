using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Out_of_Office.Application.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
    }
}
