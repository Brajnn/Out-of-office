using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Employee.Command.UpdateEmployeeStatus
{
    public class UpdateEmployeeStatusCommand:IRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }

    }
}
