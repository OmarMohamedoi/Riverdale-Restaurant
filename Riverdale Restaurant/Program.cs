namespace Riverdale_Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SEED DATA//
            Item ChickenBBQPizza = new Item(250, "Delicious Pizza with grilled chicken and BBQ", "ChickenBBQPizza", Category.MainCourse);
            Item AlfredoPasta = new Item(200, "Tasty Pasta with white sauce and chicken with mushrooms", "AlfredoPasta", Category.MainCourse);
            Item BurgerSandwich = new Item(180, "Tasty grilled burger with fresh bread and vegetables", "BurgerSandwich", Category.MainCourse);

            Item Pepsi = new Item(40, "Ice Pepsi Can", "Pepsi", Category.Drink);
            Item Miranda = new Item(40, "Ice Mirianda Can", "Miranda", Category.Drink);
            Item Sprite = new Item(40, "Ice Sprite Can", "Pepsi", Category.Drink);

            Item MoltenCake = new Item(150, "Hot chocolate cake with nutella fillings", "MoltenCake", Category.Dessert);
            Item Tirimaisu = new Item(160, "Special kind of cake with espresso and milk", "Tirimaisu", Category.Dessert);
            Item Waffle = new Item(120, "Waffle with fruits and nutella", "Waffle", Category.Dessert);

            Item FrenchFries = new Item(50, "Tasty Packet of French fries", "FrenchFries", Category.Appetizer);
            Item MozarellaFries = new Item(70, "Tasty Packet of French fries with mozarella", "MozarellaFries", Category.Appetizer);
            //////////////////////////////////////////////////////////////////////
            RestaurantBranch RiverDale = new RestaurantBranch();
            RiverDale.GetMenu().AddItem(ChickenBBQPizza);
            RiverDale.GetMenu().AddItem(AlfredoPasta);
            RiverDale.GetMenu().AddItem(BurgerSandwich);
            RiverDale.GetMenu().AddItem(Pepsi);
            RiverDale.GetMenu().AddItem(Miranda);
            RiverDale.GetMenu().AddItem(Sprite);
            RiverDale.GetMenu().AddItem(MoltenCake);
            RiverDale.GetMenu().AddItem(Tirimaisu);
            RiverDale.GetMenu().AddItem(Waffle);
            RiverDale.GetMenu().AddItem(FrenchFries);
            RiverDale.GetMenu().AddItem(MozarellaFries);
            ///////////////////////////////////////////////////////////////////////
            //Adding quantity
            foreach (var item in RiverDale.GetMenu().GetAllItems())
            {
                if (item.Category == Category.MainCourse)
                {
                    item.AddStock(10);
                }
                else if (item.Category == Category.Appetizer)
                {
                    item.AddStock(15);
                }
                else if (item.Category == Category.Drink)
                {
                    item.AddStock(30);
                }
                else
                    item.AddStock(3);
            }
            // SEED ORDERS //
            Order order1 = new Order(5); // Table 5
            order1.AddOrderLine(new OrderLine(ChickenBBQPizza, 2));
            order1.AddOrderLine(new OrderLine(Pepsi, 2));
            ChickenBBQPizza.ReduceStock(2);
            Pepsi.ReduceStock(2);
            RiverDale.AddOrder(order1);

            Order order2 = new Order(12); // Table 12
            order2.AddOrderLine(new OrderLine(AlfredoPasta, 1));
            order2.AddOrderLine(new OrderLine(Waffle, 1));
            AlfredoPasta.ReduceStock(1);
            Waffle.ReduceStock(1);
            RiverDale.AddOrder(order2);
            order2.Process(); // advance to Preparing, so Chef has something in the queue

            Order order3 = new Order(3); // Table 3
            order3.AddOrderLine(new OrderLine(BurgerSandwich, 3));
            order3.AddOrderLine(new OrderLine(FrenchFries, 3));
            BurgerSandwich.ReduceStock(3);
            FrenchFries.ReduceStock(3);
            RiverDale.AddOrder(order3);
            order3.Process(); // Pending -> Preparing
            order3.Process(); // Preparing -> Ready, so Server has something to serve
            /////////////////////////////////////////////////////////////
            ///
            Server server = new Server("Server");
            Manager manager = new Manager("Manager");
            Cheif cheif = new Cheif("Cheif");

            RiverDale.AddStaff(server);
            RiverDale.AddStaff(manager);
            RiverDale.AddStaff(cheif);


            while (true)
            {
                Console.WriteLine("What user would you like to join as: ");
                Console.WriteLine("1- Manager, 2- Server, 3-Cheif, 0-Exit Program");
                if(int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        return;
                    }
                    if (choice >0 && choice < 4)
                    {
                        switch (choice)
                        {
                            case 1:
                                manager.ShowRoleMenu(RiverDale);
                                break;
                            case 2:
                                server.ShowRoleMenu(RiverDale);
                                break;
                            case 3:
                                cheif.ShowRoleMenu(RiverDale);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        Console.WriteLine("please enter a number between 1 and 3");
                }
            }
        }
    }
}
