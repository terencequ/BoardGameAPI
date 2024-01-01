using BoardGameAPI.Controllers.Users.Models;
using BoardGameAPI.Data;
using BoardGameAPI.Data.Entities;
using BoardGameAPI.Services.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameAPI.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UserController(BoardGameAPIContext context) : ControllerBase
{
    /// <summary>
    /// Get a list of users, filtered.
    /// </summary>
    /// <param name="request">Filter options.</param>
    /// <returns>A list of users.</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(ICollection<GetUsersResponseItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetUsersRequest request)
    {
        var res = await context.User.Select(u => new GetUsersResponseItem()
        {
            Id = u.Id,
            DisplayName = u.DisplayName,
            Email = u.Email
        }).ToListAsync();
        return Ok(res);
    }
    
    /// <summary>
    /// Register as a new user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUsersResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterUsersRequest request)
    {
        // Check if a user of the same email exists
        if (await context.User.AnyAsync(u => string.Equals(u.Email, request.Email, StringComparison.CurrentCultureIgnoreCase)))
        {
            return BadRequest("User already exists.");
        }
        
        // Add user to the database, and generate a password hash
        var user = new User()
        {
            Id = Guid.NewGuid(),
            DisplayName = request.DisplayName,
            Email = request.Email?.ToLower(),
        };
        var password = new PasswordHasher<User>().HashPassword(user, request.Password ?? "");
        user.PasswordHash = password;
        context.User.Add(user);
        await context.SaveChangesAsync();
        
        // Create an auth session for the user
        var session = new UserAuthSession()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        // Send session ID back to the user
        var res = new RegisterUsersResponse()
        {
            SessionId = session.Id
        };
        return Ok(res);
    }

    /// <summary>
    /// Login as an existing user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUsersResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginUsersRequest request)
    {
        // Check if a user of the same email exists
        var user = await context.User.FirstOrDefaultAsync(u => string.Equals(u.Email, request.Email, StringComparison.CurrentCultureIgnoreCase));
        if (user == null)
        {
            return BadRequest("User does not exist.");
        }
        
        // Check if the password is correct
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password ?? "");
        if (result != PasswordVerificationResult.Success)
        {
            return BadRequest("Invalid password.");
        }
        
        // Create an auth session for the user
        var session = new UserAuthSession()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        // Send session ID back to the user
        var res = new LoginUsersResponse()
        {
            SessionId = session.Id
        };
        return Ok(res);
    }
    
    
}