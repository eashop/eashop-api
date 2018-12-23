using System.Collections.Generic;

namespace EaShop.Api.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}