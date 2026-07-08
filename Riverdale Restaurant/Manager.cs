using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Manager : Staff, IOrderCancel
    {
        public Manager(string name) : base(name)
        {
        }
        public void Restock(Item item, int quantity)
        {
            item.AddStock(quantity);
        }
        public void CancelOrder(Order order)
        {
            if (order.Status == OrderProgress.Pending || order.Status == OrderProgress.Preparing)
            {
                order.Cancel();
            }
            else
            {
                Console.WriteLine($"Cannot cancel order {order.OrderId} — current status is {order.Status}.");
            }
        }
        public IEnumerable<Order> GetServedOrders(IEnumerable<Order> orders)
        {
            List<Order> readyOrders = new List<Order>();

            foreach (var order in orders)
            {
                if (order.Status == OrderProgress.Served)
                {
                    readyOrders.Add(order);
                }
            }

            return readyOrders;
        }
        public void ShowTotalDailySales(IEnumerable<Order> orders)
        {
            decimal total = 0;
            foreach (var order in GetServedOrders(orders))
            {
                total += order.TotalOrderPrice(); // Order already knows how to total its own lines
            }
            Console.WriteLine($"Total Daily Sales: {total:C}");
        }

        public override void ShowRoleMenu(RestaurantBranch restaurantBranch)
        {
            while (true)
            {
                Console.WriteLine("1- View Total sales, 2- Restock Items, 3- Cancel order, 4- logout ");
                if(int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowTotalDailySales(restaurantBranch.GetOrders());
                            break;
                        case 2:
                            restaurantBranch.GetMenu().ShowMenu();
                            Console.WriteLine("Please enter Item ID that you want to restock");
                            if(int.TryParse(Console.ReadLine(), out int choice2))
                            {
                                Item itemptr = null!;
                                foreach (var item in restaurantBranch.GetMenu().GetAllItems())
                                {
                                    if (item.ID == choice2)
                                    {
                                        itemptr = item;
                                        break;
                                    }
                                    
                                }
                                Console.WriteLine("Please enter quantity that you want");
                                if (int.TryParse(Console.ReadLine(), out int choice3))
                                {
                                    if (choice3 > 0 && itemptr != null)
                                    {
                                        Restock(itemptr, choice3);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid item or quantity.");
                                    }
                                }
                            }
                            break;
                        case 3:
                            restaurantBranch.ShowOrders();
                            Console.WriteLine("Please enter number of order you wish to delete");
                            if(int.TryParse(Console.ReadLine(), out int choice4))
                            {
                                Order orderptr = null!;
                                foreach (var order in restaurantBranch.GetOrders())
                                {
                                    if (order.OrderId == choice4)
                                    {
                                        orderptr = order;
                                        break;
                                    }
                                }
                                if (orderptr != null)
                                {
                                    CancelOrder(orderptr);
                                }
                                else
                                {
                                    Console.WriteLine("Order not found.");
                                }
                            }
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid number");
                            break;

                    }
                }
            }
        }
    }
}
