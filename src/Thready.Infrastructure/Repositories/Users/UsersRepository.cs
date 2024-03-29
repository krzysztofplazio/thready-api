using Microsoft.EntityFrameworkCore;
using Thready.Infrastructure.Contexts;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;
using Thready.Core.Enums;

namespace Thready.Infrastructure.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ThreadyDatabaseContext _context;

    public UsersRepository(ThreadyDatabaseContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
                        =>  await _context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken).ConfigureAwait(false);

    public async Task InsertUser(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Role?> GetRoleById(int id, CancellationToken cancellationToken = default)
                        => await _context.Roles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);

    public async Task UpdateUser(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}