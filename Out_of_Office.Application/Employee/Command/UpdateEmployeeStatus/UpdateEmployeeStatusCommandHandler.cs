using MediatR;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Employee.Command.UpdateEmployeeStatus
{
    public class UpdateEmployeeStatusCommandHandler:IRequestHandler<UpdateEmployeeStatusCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeStatusCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            if (!Enum.TryParse(request.Status, out Domain.Entities.EmployeeStatus employeeStatus))
            {
                throw new ArgumentException("Invalid status.");
            }

            employee.Status = employeeStatus;
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return Unit.Value;
        }
    }
}
