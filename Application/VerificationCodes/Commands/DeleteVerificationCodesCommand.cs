using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Codes.Commands
{
    public class DeleteVerificationCodesCommand : IRequest
    {
        public required string NumberPhone { get; set; }
    }

    public class DeleteVerificationCodesCommandHandler : IRequestHandler<DeleteVerificationCodesCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteVerificationCodesCommandHandler(
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
        public async Task<Unit> Handle(DeleteVerificationCodesCommand request, CancellationToken cancellationToken)
        {
            var code = await _dbContext.Codes
            .Where(code => string.Equals(code.NumberPhone, request.NumberPhone, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(code => code.Create)
            .ToListAsync(cancellationToken);

            if (code.Any()) 
            {
                _dbContext.Codes.RemoveRange(code);
            }

            return Unit.Value;
        }
    }
}
