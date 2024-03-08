using API.Enties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")] //api/users
public class UserController : ControllerBase
{
  private readonly DataContext _context;
  public UserController(DataContext context)
  {
    this._context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
  {
    // Implementation of GetUsers method
    var users = await _context.Users.ToListAsync();
    return users;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
    return await _context.Users.FindAsync(id);
  }
}
