﻿namespace BoardGameAPI.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    
    public ICollection<UserAuthSession>? AuthSessions { get; set; }
}