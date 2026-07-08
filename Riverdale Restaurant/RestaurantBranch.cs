using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class RestaurantBranch
    {
        public string Name { get; private set; }
        public string Location { get; private set; }

        private Menu _menu = new Menu();
        public Menu GetMenu() => _menu;

        private List<Staff> _staffList = new List<Staff>();
        private List<Order> _orders = new();

        public RestaurantBranch()
        {
            Name = "RiverDale";
            Location = "NewYork";
        }
        public void AddOrder(Order order)
        {
            bool tableHasActiveOrder = false; // Only one order per table

            foreach (var o in _orders)
            {
                if (o.TableNumber == order.TableNumber &&
                    o.Status != OrderProgress.Served &&
                    o.Status != OrderProgress.Cancelled)
                {
                    tableHasActiveOrder = true;
                    break; 
                }
            }

            if (tableHasActiveOrder)
                throw new InvalidOperationException($"Table {order.TableNumber} already has an active order.");

            _orders.Add(order);
        }
        public IEnumerable<Order> GetOrders() => _orders;

        public void AddStaff(Staff staff)
        {
            _staffList.Add(staff);
        }
        public void ShowStaff()
        {
            foreach (var staff in _staffList)
            {
                staff.Print();
            }
            
        }
        public void ShowOrders()
        {
            foreach (var order in _orders)
            {
                order.DisplayOrder();
            }
        }
        public IEnumerable<Staff> GetAllStaff() => _staffList;
    }
}
