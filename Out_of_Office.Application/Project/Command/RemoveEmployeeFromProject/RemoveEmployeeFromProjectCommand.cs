using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.RemoveEmployeeFromProject
{
    public class RemoveEmployeeFromProjectCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
