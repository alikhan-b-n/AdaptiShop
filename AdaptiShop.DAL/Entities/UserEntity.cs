namespace AdaptiShop.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string PasswordHash { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; } = false;
}