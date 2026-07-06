using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Item
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; } = 0;
        public Category Category { get; set; }

        public Item(decimal price, string description, string name, Category category)
        {
            Price = price;
            Description = description;
            Name = name;
            Category = category;
        }

        public void AddStock(int amount)
        {
            Quantity += amount;
        }

        public void ReduceStock(int amount)
        {
            Quantity -= amount;
        }


    }
}
