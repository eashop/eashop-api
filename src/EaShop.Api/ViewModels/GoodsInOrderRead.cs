using EaShop.Data.Models;

namespace EaShop.Api.ViewModels
{
    public class GoodsInOrderRead : GoodsInOrderCreate
    {
        public string Name { get; set; }

        public string Image { get; set; }
    }
}
