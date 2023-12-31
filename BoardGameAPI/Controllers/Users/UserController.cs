using BoardGameAPI.Data;
using BoardGameAPI.Services.Users.Models;
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
    public async Task<ICollection<GetUsersResponseItem>> Get([FromQuery] GetUsersRequest request)
    {
        return await context.User.Select(u => new GetUsersResponseItem()
        {
            Id = u.Id,
            DisplayName = u.DisplayName,
            Email = u.Email
        }).ToListAsync();
    }
}