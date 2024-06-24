using MediatR;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.RemoveEmployeeFromProject
{
    public class RemoveEmployeeFromProjectCommandHandler : IRequestHandler<RemoveEmployeeFromProjectCommand>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public RemoveEmployeeFromProjectCommandHandler(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<Unit> Handle(RemoveEmployeeFromProjectCommand request, CancellationToken cancellationToken)
        {
            await _employeeProjectRepository.RemoveEmployeeProjectAsync(request.EmployeeId, request.ProjectId);
            return Unit.Value;
        }
    }
}
