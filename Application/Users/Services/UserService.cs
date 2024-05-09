using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _dbContext;

    public UserService(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(user));

        return user;
    }
}
