using Out_of_Office.Application.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Models.AssignProjectViewModel
{
    public class AssignProjectViewModel
    {
        public int EmployeeId { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public int SelectedProjectId { get; set; }
    }
}
