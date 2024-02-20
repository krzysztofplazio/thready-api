using Thready.Core.Enums;
using Thready.Core.Models;

namespace Thready.Core.Repositories.Users;

public interface IUsersRepository
{
    Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
    Task InsertUser(User user, CancellationToken cancellationToken = default);
    Task UpdateUser(User user, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleById(int id, CancellationToken cancellationToken = default);
}