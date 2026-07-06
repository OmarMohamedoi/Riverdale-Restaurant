using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public abstract class Staff
    {
        public string Name { get; private set; }
        public Staff(string name)
        {
            Name = name;
        }
        public virtual void Print()
        {
            Console.WriteLine($"Name: {Name}");
        }
        public abstract void ShowMenu();
    }
}
