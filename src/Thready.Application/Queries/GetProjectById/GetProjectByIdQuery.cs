using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;

namespace Thready.Application.Queries.GetProjects;

public class GetProjectByIdQuery(int id) : IRequest<ProjectResult?>
{
    public int Id { get; } = id;
}
