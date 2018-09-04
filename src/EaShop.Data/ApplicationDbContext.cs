using EaShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EaShop.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        
    }
}