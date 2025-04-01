using Microsoft.AspNetCore.Identity;

namespace IdentityCourse.Models
{
    public class UserRoleVm
    {
        public UserRoleVm()
        {
            userRoles = new List<string>(); 
        }
        public IdentityUser User { get; set; } 
        public List<string> userRoles { get; set; } 
    }
}
