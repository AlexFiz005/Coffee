using Microsoft.AspNetCore.Identity;

namespace Coffee.Models
{
    public class User : IdentityUser
    {
        public DateTime Created { get; set; }
    }
}