namespace BoardGameAPI.Data.Entities;

public class UserAuthSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
}