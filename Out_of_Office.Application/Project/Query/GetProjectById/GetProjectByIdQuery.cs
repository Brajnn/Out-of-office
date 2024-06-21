using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Query.GetProjectById
{
    public class GetProjectByIdQuery:IRequest<ProjectDto>
    {
        public int Id { get; set; }
    }
}
