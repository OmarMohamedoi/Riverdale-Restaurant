using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Item
    {
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; } = 0;
        public Category Category { get; private set; }

        public Item(decimal price, string description, string name, Category category)
        {
            Price = price;
            Description = description;
            Name = name;
            Category = category;
        }

        public void DisplayItemDetails()
        {
            Console.WriteLine($"Name: {Name}, Description: {Description}, Price: {Price}, Category: {Category}");
        }

        public void AddStock(int amount)
        {
            Quantity += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount > Quantity)
                throw new InvalidOperationException($"Cannot reduce stock below zero for {Name}.");
            Quantity -= amount;
        }


    }
}
