using JustNomApplication.Restuarant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNomApplication.Menu
{
    internal class ReadMenu
    {
        internal class MenuReader
        {
            internal List<MenuItem> Burgers; // List to store burger menu items
            internal List<MenuItem> Pizzas; // list to store pizz menu items
            internal Dictionary<string, List<string>> AvailableToppings; // Dictionary to store available toppings for each pizza
            internal Dictionary<string, List<string>> AvailableGarnishes; // Dictionary to store available garnishes for each burger
            internal List<string> AllGarnishes; // List to store all available garnishes
            internal List<string> AllToppings; // List to store all avilable toppings
            private Dictionary<string, decimal> _toppingPrices = new Dictionary<string, decimal>(); // Dictionary to store topping price
         

            public MenuReader()
            {
                Burgers = new List<MenuItem>();  // Initialize list of burgers
                Pizzas = new List<MenuItem>(); // Initialize list of pizzas
                AvailableToppings = new Dictionary<string, List<string>>(); // Initialize dictionary for available toppings
                AvailableGarnishes = new Dictionary<string, List<string>>(); // Initialize dictionary for available garnishes
                AllGarnishes = new List<string>(); // Initialize list of all garnishes
                AllToppings = new List<string>(); // Initialize list of all toppings
            }

            public void ReadMenuFromFile(string filePath)
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);   // Read all lines from the file

                    string restaurantName = "";   // Variable to store restaurant name

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Name:"))
                        {
                            restaurantName = line.Replace("Name:", "").Trim(); //Extract and store name of restaurant
                        }
                        else if (line.StartsWith("Pizza:"))
                        {
                            Pizza pizza = ParsePizza(line);
                            Pizzas.Add(pizza);
                            AvailableToppings.Add(pizza.Name, pizza.Toppings); // Store available toppings for the pizza
                        }
                        else if (line.StartsWith("Burger:"))
                        {
                            Burger burger = ParseBurger(line);
                            Burgers.Add(burger);
                            AvailableGarnishes.Add(burger.Name, burger.Garnishes); // Store available garnishes for the burger
                        }
                        if (line.StartsWith("Toppings:"))
                        {
                            // Extract toppings information from the line
                            string toppingsString = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1);
                            string[] toppingPairs = toppingsString.Split(',');
                            foreach (var pair in toppingPairs)
                            {
                                // Extract and store each topping
                                string topping = pair.Trim('<', '>').Split(',')[0].Trim();
                                AllToppings.Add(topping);


                            }
                        }
                        else if (line.StartsWith("Garnishes:"))
                        {
                            // Extract garnishes information from the line
                            string garnishesString = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1);
                            string[] garnishPairs = garnishesString.Split(',');
                            // Iterate through each garnish pair
                            foreach (var pair in garnishPairs)
                            {
                                // Extract and store each garnish
                                string garnish = pair.Trim('<', '>').Split(',')[0].Trim();
                                AllGarnishes.Add(garnish);
                            }
                        }
                    }

                    // Print the extracted information
                    Console.WriteLine($"Restaurant Name: {restaurantName}");
                    Console.WriteLine("Pizzas:");
                    foreach (var pizza in Pizzas)
                    {
                        Console.WriteLine($"{pizza.MenuText()} - Toppings: {string.Join(", ", ((Pizza)pizza).Toppings)}");
                    }
                    Console.WriteLine("Burgers:");
                    foreach (var burger in Burgers)
                    {
                        Console.WriteLine($"{burger.MenuText()} - Garnishes: {string.Join(", ", ((Burger)burger).Garnishes)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading menu from file: {ex.Message}");
                }
            }


            private Pizza ParsePizza(string line)
            {
                // Extract pizza name and price from the line
                string name = line.Substring(line.IndexOf("<Name:") + "<Name:".Length, line.IndexOf(",") - line.IndexOf("<Name:") - "<Name:".Length);
                int price = int.Parse(line.Substring(line.IndexOf("Price:") + "Price:".Length, line.IndexOf(">", line.IndexOf("Price:")) - line.IndexOf("Price:") - "Price:".Length));

                List<string> toppings = new List<string>(); // List to store pizza toppings
                int toppingsStartIndex = line.IndexOf("Toppings:[") + "Toppings:[".Length;
                int toppingsEndIndex = line.IndexOf("]", toppingsStartIndex);
                string toppingsString = line.Substring(toppingsStartIndex, toppingsEndIndex - toppingsStartIndex);

                // Extract toppings from the toppings string
                string[] toppingPairs = toppingsString.Split(',');
                foreach (var pair in toppingPairs)
                {
                    string[] parts = pair.Trim('<', '>').Split(',');
                    string topping = parts[0].Trim(); // Extract the topping name from each pair
                    
                    
                    toppings.Add(topping);
                  
                }

                return new Pizza(name, price, toppings);
            }

            private Burger ParseBurger(string line)
            {
                string name = line.Substring(line.IndexOf("<Name:") + "<Name:".Length, line.IndexOf(",") - line.IndexOf("<Name:") - "<Name:".Length);
                int price = int.Parse(line.Substring(line.IndexOf("Price:") + "Price:".Length, line.IndexOf(">", line.IndexOf("Price:")) - line.IndexOf("Price:") - "Price:".Length));

                List<string> garnishes = new List<string>();
                List<int> garnishPrices = new List<int>(); // New list to store garnish prices

                int garnishesStartIndex = line.IndexOf("Garnishes:[") + "Garnishes:[".Length;
                int garnishesEndIndex = line.IndexOf("]", garnishesStartIndex);
                string garnishesString = line.Substring(garnishesStartIndex, garnishesEndIndex - garnishesStartIndex);

                // Split the garnishes string into pairs of garnish and price
                string[] garnishPairs = garnishesString.Split(',');
                foreach (var pair in garnishPairs)
                {
                    string[] parts = pair.Trim('<', '>').Split(',');
                    string garnish = parts[0].Trim(); // Extract the garnish name from each pair
                   

                    garnishes.Add(garnish);
                   
                }

                return new Burger(name, price, garnishes, garnishPrices); // Pass both garnishes and their prices to the Burger constructor
            }

            public void AddToppingPrice(string topping, decimal price)
            {
                _toppingPrices[topping] = price;
            }
            public decimal GetToppingPrice(string topping)
            {
                // Check if topping exists in the dictionary
                if (_toppingPrices.ContainsKey(topping))
                {
                    return _toppingPrices[topping];
                }
                else
                {
                    // Default to 0 if topping price not found
                    return 0;
                }
            }
        }
    }
}
