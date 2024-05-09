using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User> GetById(Guid id, CancellationToken cancellationToken);
}
