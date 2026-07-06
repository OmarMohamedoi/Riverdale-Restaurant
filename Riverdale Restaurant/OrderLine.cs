using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class OrderLine
    {
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotal => Item.Price * Quantity;

        public OrderLine(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
