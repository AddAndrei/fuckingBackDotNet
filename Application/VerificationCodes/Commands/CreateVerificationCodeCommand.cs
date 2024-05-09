using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Codes.Commands;

public class CreateVerificationCodeCommand : IRequest<Unit>
{
    public required string NumberPhone { get; set; }
}

public class CreateVerificationCodeCommandHandler : IRequestHandler<CreateVerificationCodeCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateVerificationCodeCommandHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Содержит логику обработки команды
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Unit> Handle(CreateVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var code = new VerificationCode()
        {
            Id = Guid.NewGuid(),
            Create = DateTime.Now,
            Update = DateTime.Now,
            Code = "6666",
            NumberPhone = request.NumberPhone
        };
        _dbContext.Codes.Add(code);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}