using Microsoft.EntityFrameworkCore;
using Thready.API.Contexts;
using Thready.Models.Models;

namespace Thready.API.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ThreadyDatabaseContext _context;

    public UsersRepository(ThreadyDatabaseContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default) 
                        => await _context.Users.SingleOrDefaultAsync(x => x.Username == username, cancellationToken).ConfigureAwait(false);

    public async Task InsertUser(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Role?> GetRoleByRoleName(string name, CancellationToken cancellationToken = default) 
                        => await _context.Roles.SingleOrDefaultAsync(x => x.Name == name, cancellationToken).ConfigureAwait(false);
}

