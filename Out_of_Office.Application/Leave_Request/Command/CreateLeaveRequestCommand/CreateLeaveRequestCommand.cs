using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Leave_Request.Command.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public string AbsenceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
