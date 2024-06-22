using MediatR;
using Out_of_Office.Application.Common.Exceptions;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.UpdateProjectStatus
{
    public class UpdateProjectStatusCommandHandler: IRequestHandler<UpdateProjectStatusCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectStatusCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            project.Status = request.Status;

            await _projectRepository.UpdateProjectAsync(project);

            return Unit.Value;
        }
    }
}
