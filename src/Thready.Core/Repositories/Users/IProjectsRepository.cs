using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;
using Thready.Core.Dtos.Paginations;
using Thready.Core.Models;

namespace Thready.Core.Repositories.Users;

public interface IProjectsRepository
{
    Task<PagedItems<ProjectResult>> GetAssignedProjects(string? order, string? search, int userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<ProjectResult?> GetProjectById(int id, CancellationToken cancellationToken = default);
    Task InsertProject(Project project, CancellationToken cancellationToken);
}
