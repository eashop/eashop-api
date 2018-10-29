using System;
using System.Collections.Generic;

namespace EaShop.Api.ViewModels
{
    public class OrderRead
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string ShipmentMethod { get; set; }

        public virtual IEnumerable<GoodsInOrderRead> GoodsInOrder { get; set; }
    }
}