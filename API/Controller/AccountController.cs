using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Enties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller;

public class AccountController : BaseApiController
{

  public readonly DataContext _context;

  public AccountController(DataContext context)
  {
    _context = context;
  }

  [HttpPost("register")] // posy: api/accpunt/register

  public async Task<ActionResult<AppUser>> Register(RegisterDtos registerDto)
  {

    if (await UserExists(registerDto.username)) return BadRequest("username exist");
    using var hmc = new HMACSHA512();
    var user = new AppUser
    {
      UserName = registerDto.username.ToLower(),
      passwordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Passoword)),
      passwordSalt = hmc.Key
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return user;
  }

  private async Task<bool> UserExists(string username)
  {
    return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
  }
}
