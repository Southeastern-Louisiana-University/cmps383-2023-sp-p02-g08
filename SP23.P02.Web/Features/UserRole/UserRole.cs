using Microsoft.AspNetCore.Identity;

public class UserRole : IdentityUserRole<int>
    {
    public string RoleName { get; set; }
    }
