using System.Collections.ObjectModel;
using System.Data;
using Thready.Core.Enums;

namespace Thready.Core.Models;

public class Role
{
    public int Id { get; set; }
    public RoleEnum Name { get; set; }
    public int Priority { get; set; }
    public ICollection<User> Users { get; set; } = [];
}