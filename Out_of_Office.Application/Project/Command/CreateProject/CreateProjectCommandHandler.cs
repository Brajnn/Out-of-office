using MediatR;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Command.CreateProject
{
    public class CreateProjectCommandHandler:IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Domain.Entities.Project
            {
                ProjectType = request.ProjectType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ProjectManagerID = request.ProjectManagerID,
                Comment = request.Comment,
                Status = request.Status
            };

            await _projectRepository.AddProjectAsync(project);
            return Unit.Value;

        }
        
    }
}
