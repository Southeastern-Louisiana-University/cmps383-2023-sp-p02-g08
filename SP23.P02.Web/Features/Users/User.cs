using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
   
    public string Name { get; set; }
    public string password { get; set; }


}