using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Profile> Profiles { get; }
    DbSet<VerificationCode> Codes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
