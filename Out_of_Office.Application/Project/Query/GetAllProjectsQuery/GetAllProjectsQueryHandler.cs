using AutoMapper;
using MediatR;
using Out_of_Office.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Out_of_Office.Application.Project.Query.GetAllProjectsQuery
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            if (request.UserRole == "Employee")
            {
                var employeeProjects = await _employeeProjectRepository.GetProjectsByEmployeeIdAsync(request.UserId);
                projects = projects.Where(p => employeeProjects.Any(ep => ep.ProjectId == p.ID)).ToList();
            }

            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            return projectDtos;
        }
    }
}
