using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Codes.Commands;

public class VerificateVerificationCodeCommand : IRequest
{
    public required string NumberPhone { get; set; }
    public required string Code { get; set; }
}

public class VerificateVerificationCodeCommandHandler : IRequestHandler<VerificateVerificationCodeCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public VerificateVerificationCodeCommandHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///     Содержит логику обработки команды
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Unit> Handle(VerificateVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var code = await _dbContext.Codes
            .Where(code => code.NumberPhone == request.NumberPhone && code.Code == request.Code)
            .OrderBy(code => code.Create)
            .LastOrDefaultAsync(cancellationToken);

        if (code == null)
            throw new NotFoundException(nameof(code));

        _dbContext.Codes.Remove(code);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
