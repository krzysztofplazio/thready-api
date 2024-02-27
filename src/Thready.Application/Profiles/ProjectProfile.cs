using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Commands.AddProject;
using Thready.Application.Dtos.Projects;
using Thready.Core.Models;

namespace Thready.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<AddProjectCommand, Project>();
        CreateMap<ProjectPermissionDto, ProjectPermission>();
    }
}
