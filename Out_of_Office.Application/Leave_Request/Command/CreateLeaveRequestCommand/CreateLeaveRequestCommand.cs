using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Out_of_Office.Application.Attributes;

namespace Out_of_Office.Application.Leave_Request.Command.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommand : IRequest
    {
        public int EmployeeId { get; set; }
        [Required]
        [DisplayName("Absence Reason")]
        public string AbsenceReason { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]

        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        [DateRange("StartDate", ErrorMessage = "The end date must be later than the start date.")]
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
