using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Employee.Command.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest
    {
        public string FullName { get; set; }
        public string Subdivision { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public int PeoplePartnerID { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public byte[] Photo { get; set; }
        public CreateEmployeeCommand() { }

        public CreateEmployeeCommand(EmployeeDto employee)
        {
            FullName = employee.FullName;
            Subdivision = employee.Subdivision;
            Position = employee.Position;
            Status = employee.Status;
            PeoplePartnerID = employee.PeoplePartnerID;
            OutOfOfficeBalance = employee.OutOfOfficeBalance;
            Photo = employee.Photo;
        }
    }
}
