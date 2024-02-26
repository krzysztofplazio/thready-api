using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;
using Thready.Core.Repositories.Users;

namespace Thready.Application.Queries.GetProjects
{
    internal class GetProjectByIdHandler(IProjectsRepository projectsRepository) : IRequestHandler<GetProjectByIdQuery, ProjectResult?>
    {
        private readonly IProjectsRepository _projectsRepository = projectsRepository;

        public async Task<ProjectResult?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _projectsRepository.GetProjectById(request.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
