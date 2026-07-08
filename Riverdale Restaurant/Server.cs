using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{

    public class Server : Staff, IOrderCancel
    {
        
        public Server(string name) :base(name)
        {

        }

        public IEnumerable<Order> GetReadyOrders(IEnumerable<Order> orders)
        {
            List<Order> readyOrders = new List<Order>();

            foreach (var order in orders)
            {
                if (order.Status == OrderProgress.Ready)
                {
                    readyOrders.Add(order);
                }
            }

            return readyOrders;
        }
        public void ViewReadyOrders(IEnumerable<Order> orders)
        {
            Console.WriteLine("=========Showing Ready Orders============");
            foreach (var order in GetReadyOrders(orders))
            {
                order.DisplayOrder();
            }
        }
        public void ServeOneOrder(Order order)
        {
            if (order.Status == OrderProgress.Ready)
            {
                order.Process();
                Console.WriteLine($"Order served successfully with a bill of {order.TotalOrderPrice()}");
            }
            else throw new Exception("Server Can only serve ready orders."); 
        }

        public void ServeAllOrders(IEnumerable<Order> orders)
        {
            var readyOrders = GetReadyOrders(orders);

            foreach (var order in readyOrders)
            {
                order.Process(); 
            }
        }
        public void CreateOrder(RestaurantBranch restaurantBranch)
        {
            Console.Write("Enter table number: ");
            if (!int.TryParse(Console.ReadLine(), out int tableNumber))
            {
                Console.WriteLine("Invalid table number.");
                return;
            }
            Order newOrder = new Order(tableNumber);
            while (true)
            {
                bool found = false;   
                Console.WriteLine("Please enter the ItemID You wish to add or 0 to finish order.");
                if(int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        break;
                    }
                    Item itemptr = null!;
                   // bool found = false;
                    foreach (var item in restaurantBranch.GetMenu().GetAllItems())
                    {
                        if (item.ID == choice)
                        {
                            itemptr = item;
                            found = true;
                            break;
                        }
                    }
                    if (found == false) {
                        Console.WriteLine( "Item not found");
                        continue;
                    }

                    Console.WriteLine("Please enter quantity");
                    if(int.TryParse(Console.ReadLine(), out int choice2))
                    {
                        if (choice2 <= 0)
                        {
                            Console.WriteLine("Please enter a valid positive value.");
                        }
                        else if (choice2 > itemptr.Quantity)
                        {
                            Console.WriteLine("Not enough stock");
                        }
                        else 
                        {
                            newOrder.AddOrderLine(new OrderLine(itemptr, choice2));
                            itemptr.ReduceStock(choice2);
                        }
                    }
                    

                }
            }
            List<OrderLine> lines = new List<OrderLine>(newOrder.GetOrderLines());
            if (lines.Count == 0)
            {
                Console.WriteLine("Order cancelled — no items were added.");
                return;
            }
            try
            {
                restaurantBranch.AddOrder(newOrder);
                Console.WriteLine("Order placed successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public override void ShowRoleMenu(RestaurantBranch restaurantBranch)
        {
            while (true)
            {
                Console.WriteLine("1-Show customer the menu, 2-Let Customer search with category, 3- Show Ready Orders, 4-Serve one order, 5-Serve all orders, 6-Take customer's Order, 7- Logout");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            restaurantBranch.GetMenu().ShowMenu();
                            break;
                        case 2:
                            Console.WriteLine("Pick a category");
                            Console.WriteLine("1- Appetizer, 2- MainCourse, 3- Drink, 4- Dessert");
                            if(int.TryParse(Console.ReadLine(), out int choice2)) {
                                switch (choice2)
                                {
                                    case 1:
                                        restaurantBranch.GetMenu().SearchByCategory(Category.Appetizer);
                                        break;
                                    case 2:
                                        restaurantBranch.GetMenu().SearchByCategory(Category.MainCourse);
                                        break;
                                    case 3:
                                        restaurantBranch.GetMenu().SearchByCategory(Category.Drink);
                                        break;
                                    case 4:
                                        restaurantBranch.GetMenu().SearchByCategory(Category.Dessert);
                                        break;

                                }
                            }
                            else { Console.WriteLine("Invalid input"); }
                            break;
                        case 3:
                            ViewReadyOrders(restaurantBranch.GetOrders());
                            break;
                        case 4:
                            var readyOrders = GetReadyOrders(restaurantBranch.GetOrders());

                            Console.WriteLine("=== Ready Orders ===");
                            foreach (var o in readyOrders)
                            {
                                o.DisplayOrder();
                            }

                            Console.Write("Enter Order ID to serve: ");
                            if (int.TryParse(Console.ReadLine(), out int orderId))
                            {
                                Order selected = null;
                                foreach (var o in readyOrders)
                                {
                                    if (o.OrderId == orderId)
                                    {
                                        selected = o;
                                        break;
                                    }
                                }

                                if (selected != null)
                                {
                                    ServeOneOrder(selected);
                                }
                                else
                                {
                                    Console.WriteLine("Order not found or not ready.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                            break;
                        case 5:
                            ServeAllOrders(restaurantBranch.GetOrders());
                            break;
                        case 6:
                            CreateOrder(restaurantBranch);
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }


                }
                else { Console.WriteLine("Please enter a valid number"); }
            }
            
        }

        public void CancelOrder(Order order)
        {
            if (order.Status == OrderProgress.Pending)
            {
                order.Cancel();
            }
            else
            {
                Console.WriteLine($"Cannot cancel order {order.OrderId} — status is {order.Status}.");
            }
        }
    }
}
