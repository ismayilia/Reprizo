using Microsoft.AspNetCore.Identity;

namespace Reprizo.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
