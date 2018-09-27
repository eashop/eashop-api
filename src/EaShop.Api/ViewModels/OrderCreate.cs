using System;
using System.Collections.Generic;

namespace EaShop.Api.ViewModels
{
    public class OrderCreate
    {
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual IEnumerable<GoodsInOrderCreate> GoodsInOrder { get; set; }
    }
}
