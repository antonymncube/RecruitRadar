using API.Enties;
using Microsoft.AspNetCore.Mvc;

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
  public ActionResult<IEnumerable<AppUser>> GetUsers()
  {
    // Implementation of GetUsers method
    var users = _context.Users.ToList();
    return users;
  }

  [HttpGet("{id}")]
  public ActionResult<AppUser> GetUser(int id)
  {
    return _context.Users.Find(id);
  }
}
