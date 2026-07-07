using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Server : Staff
    {
        //private List<Order> _orders = new();
        Server(string name) :base(name)
        {

        }
        public void ViewReadyOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine("=========Showing Ready Orders============");
                if (order.Status == OrderProgress.Ready)
                {
                    order.DisplayOrder();
                }
            }
        }
        public void ServeOneOrder(Order order)
        {
            order.Process();
        }

        public void ServeAllOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {

            }
        }
        public override void ProcessRoleMenu()
        {
            throw new NotImplementedException();
        }

        public override void ShowRoleMenu()
        {
            throw new NotImplementedException();
        }
    }
}
