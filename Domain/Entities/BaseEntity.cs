using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BaseEntity
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Дата создания объекта
    /// </summary>
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Create { get; set; }

    /// <summary>
    ///     Дата обновления объекта
    /// </summary>
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Update { get; set; }

    /// <summary>
    ///     Дата удаления объекта
    /// </summary>
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Delete { get; set; }
}
