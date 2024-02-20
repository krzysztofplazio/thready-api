using Thready.Core.Enums;

namespace Thready.Core.Models;

public class Role
{
    public int Id { get; set; }
    public RoleEnum Name { get; set; }
    public int Priority { get; set; }
    public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
}