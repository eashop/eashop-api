using Microsoft.AspNetCore.Identity;

namespace EaShop.Data.Models
{
    public class ApplicationUser: IdentityUser, IApplicationDbContext
    {
        
    }
}