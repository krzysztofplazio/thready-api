using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Core.Models;

namespace Thready.Application.Dtos.Projects;

public class ProjectResult
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public string Creator { get; set; } = null!;
}
