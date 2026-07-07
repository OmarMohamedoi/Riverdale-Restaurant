using System;
using System.Collections.Generic;
using System.Text;

namespace Riverdale_Restaurant
{
    public class Menu
    {
        private List<Item> _items = new();


        public void ShowMenu()
        {
            Console.WriteLine("======MENU=======");
            foreach (var item in _items)
            {
                item.DisplayItemDetails();
            }
        }
        public void SearchByCategory(Category category)
        {
            Console.WriteLine($"=======Searching with Category: {category}==========");
            foreach (var item in _items)
            {
                if (item.Category == category)
                {
                    item.DisplayItemDetails();
                }

            }
        }
        public void AddItem(Item item) => _items.Add(item);
        public IEnumerable<Item> GetAllItems() => _items;
    }
}
