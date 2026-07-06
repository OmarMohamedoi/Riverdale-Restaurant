using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Order
    {
        private List<OrderLine> _orderLines = new List<OrderLine>();
        public int TableNumber { get; private set; }

        private OrderProgress status;

        public void Process()
        {
            status = status switch
            {
                OrderProgress.Pending => OrderProgress.Preparing,
                OrderProgress.Preparing => OrderProgress.Ready,
                OrderProgress.Ready => OrderProgress.Served,
                _ => throw new InvalidOperationException("Order already served.")
            };
        }

        public void Cancel()
        {
            foreach (OrderLine line in _orderLines)
            {
                line.Item.AddStock(line.Quantity); 
            }
            status = OrderProgress.Cancelled;
        }

    }
}
