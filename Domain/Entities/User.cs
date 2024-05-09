namespace Domain.Entities;

public class User : BaseEntity
{
    public Profile Profile { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public User(string Login, string Password)
    {
        this.Login = Login;
        this.Password = Password;
    }
}
