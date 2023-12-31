using BoardGameAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameAPI.Data;

/// <summary>
/// Main Entity Framework Core class for interfacing with the database.
/// </summary>
public class BoardGameAPIContext : DbContext
{

    public required DbSet<User> User { get; set; }
}