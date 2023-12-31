namespace BoardGameAPI.Services.Users.Models;

public class GetUsersResponseItem
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
}