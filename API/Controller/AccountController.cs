using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Enties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
      _context = context;
    }

    [HttpPost("register")] // POST: api/account/register
    public async Task<ActionResult<AppUser>> RegisterAsync(RegisterDtos registerDto)
    {
      if (await UserExists(registerDto.UserName))
        return BadRequest("Username already exists.");

      using var hmac = new HMACSHA512();
      var user = new AppUser
      {
        UserName = registerDto.UserName.ToLower(),
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        passwordSalt = hmac.Key
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
}
