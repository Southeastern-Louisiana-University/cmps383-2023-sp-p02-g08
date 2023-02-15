using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();

}
public class CreateUserDto
{


    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required, MinLength(1)]
    public string[] Roles { get; set; } = Array.Empty<string>();
}