using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public required string NumberPhone { get; set; }

    public required string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}

public class CreateNewsCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICryptoService _cryptoService;

    public CreateNewsCommandHandler(
        IApplicationDbContext dbContext, ICryptoService cryptoService)
    {
        _dbContext = dbContext;
        _cryptoService = cryptoService;
    }

    /// <summary>
    ///     Содержит логику обработки команды
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Login == request.NumberPhone, cancellationToken);
        if (user != null)
            throw new Exception(string.Format("Пользователь с логином {0} уже существует.", request.NumberPhone));

        user = new User(request.NumberPhone, _cryptoService.GetMD5Hash(request.Password)) 
        {
            Id = Guid.NewGuid(),
            Create = DateTime.Now,
            Update = DateTime.Now
        };

        var profile = new Profile()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            User = user,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        _dbContext.Users.Add(user);
        _dbContext.Profiles.Add(profile);
  
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
