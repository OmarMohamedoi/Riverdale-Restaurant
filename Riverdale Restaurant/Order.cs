using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Order
    {
        private static int _nextOrderId = 1;
        public int OrderId { get; private set; }
        private List<OrderLine> _orderLines = new List<OrderLine>();
        public int TableNumber { get; private set; }

        private OrderProgress status;

        public OrderProgress Status => status;

        public IEnumerable<OrderLine> GetOrderLines() => _orderLines;


        public Order(int tableNumber)
        {
            if (tableNumber < 1 || tableNumber > 20)
                throw new ArgumentException("TableNumber must be from 1 to 20");
            TableNumber = tableNumber;
            status = OrderProgress.Pending;
            OrderId = _nextOrderId; 
            _nextOrderId++;
        }
        public void DisplayOrder()
        {
            Console.WriteLine($"Order number: {OrderId}, Table Number: {TableNumber}, Status: {Status}");
        }
        public decimal TotalOrderPrice()
        {
            decimal total = 0;
            foreach (var item in _orderLines)
            {
                total += item.LineTotal;
            }
            return total;
        }
        public void AddOrderLine(OrderLine orderLine)
        {
            _orderLines.Add(orderLine);
        }

        public void Process()
        {
            status = status switch
            {
                OrderProgress.Pending => OrderProgress.Preparing,
                OrderProgress.Preparing => OrderProgress.Ready,
                OrderProgress.Ready => OrderProgress.Served,
                _ => throw new InvalidOperationException($"Cannot process an order with status {status}.")
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
