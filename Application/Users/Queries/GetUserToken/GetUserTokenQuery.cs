using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<string>
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}

public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, string>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICryptoService _cryptoService;
    private readonly IJwtProvider _jwtProvider;

    public GetUserTokenQueryHandler(
        IApplicationDbContext dbContext, ICryptoService cryptoService, IJwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _cryptoService = cryptoService;
        _jwtProvider = jwtProvider;
    }

    /// <summary>
    ///     Содержит логику обработки команды
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<string> Handle(GetUserTokenQuery query, CancellationToken cancellationToken)
    {
        var password = _cryptoService.GetMD5Hash(query.Password);
        var user = await _dbContext.Users
            .Where(user => user.Login == query.Login && user.Password == password)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(user));

        return _jwtProvider.GenerateJwtToken(user);
    }
}
