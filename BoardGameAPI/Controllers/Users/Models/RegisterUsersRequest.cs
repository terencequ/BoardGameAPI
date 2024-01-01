namespace BoardGameAPI.Controllers.Users.Models;

public class RegisterUsersRequest
{
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}