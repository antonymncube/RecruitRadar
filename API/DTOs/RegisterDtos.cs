using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace API
{
  public class RegisterDtos
  {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
