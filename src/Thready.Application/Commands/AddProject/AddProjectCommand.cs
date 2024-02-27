using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;
using Thready.Core.Models;

namespace Thready.Application.Commands.AddProject;

public class AddProjectCommand(string title, string description, DateTime dueDate, int creatorId, ICollection<ProjectPermissionDto> projectPermissions) : IRequest<int>
{
    public string Title { get; } = title;
    public string Description { get; } = description;
    public DateTime DueDate { get; } = dueDate;
    public int CreatorId { get; } = creatorId;
    public ICollection<ProjectPermissionDto> ProjectPermissions { get; } = projectPermissions;
}
