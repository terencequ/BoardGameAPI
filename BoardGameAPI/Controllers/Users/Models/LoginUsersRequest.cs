namespace BoardGameAPI.Controllers.Users.Models;

public class LoginUsersRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}