using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class RestaurantBranch
    {
        public string Name { get; private set; }
        public string Location { get; private set; }

        private List<Staff> _staffList = new List<Staff>();

        public RestaurantBranch()
        {
            Name = "RiverDale";
            Location = "NewYork";
        }

        public void AddStaff(Staff staff)
        {
            _staffList.Add(staff);
        }
        public void ShowStaff(List<Staff> staffList)
        {
            foreach (var staff in staffList)
            {
                staff.Print();
            }
            
        }
    }
}
