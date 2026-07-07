using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Cheif : Staff
    {
        public Cheif(string name) : base(name)
        {
        }

        public void ViewPendingQueue(IEnumerable<Order> orders)
        {
            Console.WriteLine("========Kitchen Pending Queue =========");
            foreach (var order in orders)
            {
                
                if (order.Status == OrderProgress.Pending)
                {
                    order.DisplayOrder();
                }
            }
        }

        public void ViewOrdersReadyForProcessing(IEnumerable<Order> orders)
        {
            Console.WriteLine("========Kitchen Pending Queue =========");
            foreach (var order in orders)
            {
                
                if (order.Status == OrderProgress.Pending || order.Status == OrderProgress.Preparing)
                {
                    order.DisplayOrder();
                }
            }
        }


        public void ProcessOneOrder(Order order)
        {
            if (order.Status == OrderProgress.Pending || order.Status == OrderProgress.Preparing)
            {
                order.Process();
            }
            else throw new Exception("Cheif can only work on pending/preparing orders.");
        }


        

        public override void ShowRoleMenu(RestaurantBranch restaurantBranch)
        {
            while (true) { 
            Console.WriteLine("1. View Pending Orders, 2. Process Order, 3. Logout");
            if(int.TryParse(Console.ReadLine(), out int choice))
            {
                    switch (choice)
                    {
                        case 1:
                            ViewPendingQueue(restaurantBranch.GetOrders());
                            break;
                        case 2:
                            ViewOrdersReadyForProcessing(restaurantBranch.GetOrders());
                            Console.WriteLine("Please enter order number you want to process");
                            if (int.TryParse(Console.ReadLine(), out int choice2))
                            {
                                Order orderptr = null!;
                                foreach (var order in restaurantBranch.GetOrders())
                                {
                                    if (order.OrderId == choice2)
                                    {
                                        orderptr = order;
                                        break;
                                    }
                                }
                                if (orderptr != null)
                                {
                                    ProcessOneOrder(orderptr);
                                }
                            }
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
        }
    }
}
