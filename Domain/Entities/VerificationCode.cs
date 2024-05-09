namespace Domain.Entities;

public class VerificationCode : BaseEntity
{
    /// <summary>
    /// Проверочный код
    /// </summary>
    public required string NumberPhone { get; set; } = string.Empty;

    /// <summary>
    /// Проверочный код
    /// </summary>
    public required string Code { get; set; } = string.Empty;
}
