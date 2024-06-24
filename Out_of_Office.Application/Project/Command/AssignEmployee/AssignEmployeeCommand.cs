using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.AssignEmployee
{
    public class AssignEmployeeCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
