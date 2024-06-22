using MediatR;
using Out_of_Office.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.UpdateProjectStatus
{
    public class UpdateProjectStatusCommand:IRequest
    {
        public int ProjectId { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
