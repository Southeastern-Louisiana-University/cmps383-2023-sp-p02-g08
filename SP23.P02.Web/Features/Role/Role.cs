using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<int>
 {
    public string RoleName { get; set; }
 }

