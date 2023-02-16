using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Users
{
    public class User : IdentityUser<int>
    {
        // Why you can't initialize this with a blank ICollection baffles me
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}