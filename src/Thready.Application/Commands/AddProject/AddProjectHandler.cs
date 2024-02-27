using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;

namespace Thready.Application.Commands.AddProject;

public class AddProjectHandler(IProjectsRepository projectsRepository, IMapper mapper) : IRequestHandler<AddProjectCommand, int>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<Project>(request);
        project.ProjectPermissions.Add(new ProjectPermission { UserId =  project.CreatorId });
        await _projectsRepository.InsertProject(project, cancellationToken).ConfigureAwait(false);
        return project.Id;
    }
}
