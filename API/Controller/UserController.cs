using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")] //api/users
public class UserController : ControllerBase
{
  private readonly DataContext _context;
  public UserController(DataContext context)
  {
    this._context = context;
  }
}
