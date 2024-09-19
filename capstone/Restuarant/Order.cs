using JustNomApplication.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JustNomApplication.Menu.ReadMenu;

// This class represents an order in the restaurant.
// It allows customers to place orders for burgers and pizzas, add or remove toppings/garnishes, 
// display all orders, and save order details to a file.

namespace JustNomApplication.Restuarant
{
    internal class Order : ConsoleMenu
    {
        private MenuReader _menuReader; // MenuReader object to access menu items and toppings/garnishes
        private List<MenuItem> _orders; // List to store ordered items
        private string _customerName; // Customer's name
        private bool _delivery; // Indicates whether the order is for delivery
        private string _deliveryAddress; // Delivery address if applicable

        // Constructor to initialize the Order object
        public Order(MenuReader menuReader)
        {
            _menuReader = menuReader; // Initialize the MenuReader
            _orders = new List<MenuItem>(); // Initialize the list of orders
        }

        // Override method to display the main menu
        public override string MenuText()
        {
            return "Welcome to Our Shop!"; // Display welcome message
        }

        // Method to create the menu options and prompt user inputs
        public override void CreateMenu()
        {
            // Check if customer name is not provided yet
            if (_customerName == null)
            {
                Console.WriteLine("Please enter your name:"); // Prompt for customer's name
                _customerName = Console.ReadLine(); // Read customer's name from input

                Console.WriteLine("Do you want delivery? (Y/N)"); // Prompt for delivery option
                _delivery = ConsoleHelpers.GetYesNoInput(""); // Read delivery option from input

                // If delivery is chosen, prompt for delivery address
                if (_delivery)
                {
                    Console.WriteLine("Enter your delivery address:");
                    _deliveryAddress = Console.ReadLine();
                }
            }

            // Display the main menu options
            Console.WriteLine("Please select an item:");
            Console.WriteLine("1. Burger");
            Console.WriteLine("2. Pizza");
            Console.WriteLine("3. Display All Orders");
            Console.WriteLine("4. Exit");
        }

        // Override method to handle user selection from the main menu
        public override void Select()
        {
            IsActive = true; // Set the menu to active
            do
            {
                CreateMenu(); // Display the main menu
                int selection = ConsoleHelpers.GetIntegerInRange(1, 4, this.ToString()); // Get user selection

                // Switch based on user selection
                switch (selection)
                {
                    case 1:
                        OrderBurger(); // Call method to place burger order
                        break;
                    case 2:
                        OrderPizza(); // Call method to place pizza order
                        break;
                    case 3:
                        DisplayOrders(); // Call method to display all orders
                        break;
                    case 4:
                        IsActive = false; // Exit the menu loop
                        break;
                }
            } while (IsActive);
        }

        // Method to handle ordering burgers
        private void OrderBurger()
        {
            // Display available burger options
            Console.WriteLine("Available Burgers:");
            for (int i = 0; i < _menuReader.Burgers.Count; i++)
            {
                var burgerItem = (Burger)_menuReader.Burgers[i];
                Console.WriteLine($"{i + 1}. {burgerItem.MenuText()} - Garnishes: {string.Join(", ", burgerItem.Garnishes)}");
            }
            Console.WriteLine("Select a Burger by entering its number:"); // Prompt for burger selection
            int selection = ConsoleHelpers.GetIntegerInRange(1, _menuReader.Burgers.Count, "Select a Burger:");
            Console.WriteLine("Burger ordered:");
            var burger = (Burger)_menuReader.Burgers[selection - 1];

            // Ask user if they want to add or remove garnishes
            Console.WriteLine("Do you want to add or remove any garnish? (A to add, R to remove, any other key to skip)");
            string choice = Console.ReadLine()?.Trim().ToUpper();
            switch (choice)
            {
                case "A":
                    AddGarnish(burger); // Call method to add garnish
                    break;
                case "R":
                    RemoveGarnish(burger); // Call method to remove garnish
                    break;
            }
            burger.Select(); // Select the burger
            _orders.Add(burger); // Add the burger to the list of orders
        }

        // Method to handle ordering pizzas
        private void OrderPizza()
        {
            // Display available pizza options
            Console.WriteLine("Available Pizzas:");
            for (int i = 0; i < _menuReader.Pizzas.Count; i++)
            {
                var pizzaItem = (Pizza)_menuReader.Pizzas[i];
                Console.WriteLine($"{i + 1}. {pizzaItem.MenuText()} - Toppings: {string.Join(", ", pizzaItem.Toppings)}");
            }
            Console.WriteLine("Select a Pizza by entering its number:"); // Prompt for pizza selection
            int selection = ConsoleHelpers.GetIntegerInRange(1, _menuReader.Pizzas.Count, "Select a Pizza:");
            var pizza = (Pizza)_menuReader.Pizzas[selection - 1];

            // Ask user if they want to add or remove toppings
            Console.WriteLine("Do you want to add or remove toppings? (A to add, R to remove, any other key to skip)");
            string choice = Console.ReadLine()?.Trim().ToUpper();
            switch (choice)
            {
                case "A":
                    AddToppings(pizza); // Call method to add toppings
                    break;
                case "R":
                    RemoveToppings(pizza); // Call method to remove toppings
                    break;
            }

            pizza.Select(); // Select the pizza
            _orders.Add(pizza); // Add the pizza to the list of orders
        }

        // Method to add toppings to a pizza
        private void AddToppings(Pizza pizza)
        {
            Console.WriteLine("Available Toppings:");
            for (int i = 0; i < _menuReader.AllToppings.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuReader.AllToppings[i]}");
            }

            int selectedToppingIndex = ConsoleHelpers.GetIntegerInRange(1, _menuReader.AllToppings.Count, "Enter the topping number you want to add:") - 1;

            string selectedTopping = _menuReader.AllToppings[selectedToppingIndex];
            pizza.Toppings.Add(selectedTopping);

            Console.WriteLine($"Topping '{selectedTopping}' added to the pizza.");
        }

        // Method to remove toppings from a pizza
        private void RemoveToppings(Pizza pizza)
        {
            if (pizza.Toppings.Count == 0)
            {
                Console.WriteLine("The pizza has no toppings to remove.");
                return;
            }

            Console.WriteLine("Current Toppings:");
            for (int i = 0; i < pizza.Toppings.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pizza.Toppings[i]}");
            }

            Console.WriteLine("Enter the number of the topping you want to remove:");
            int toppingIndexToRemove = ConsoleHelpers.GetIntegerInRange(1, pizza.Toppings.Count, "Enter the topping number to remove:");

            string removedTopping = pizza.Toppings[toppingIndexToRemove - 1];
            pizza.Toppings.RemoveAt(toppingIndexToRemove - 1);
            Console.WriteLine($"Topping '{removedTopping}' removed from the pizza.");
        }

        // Method to add garnish to a burger
        private void AddGarnish(Burger burger)
        {
            Console.WriteLine("Available Garnishes:");
            for (int i = 0; i < _menuReader.AllGarnishes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuReader.AllGarnishes[i]}");
            }

            int selectedGarnishIndex = ConsoleHelpers.GetIntegerInRange(1, _menuReader.AllGarnishes.Count, "Enter the garnish number you want to add:") - 1;

            string selectedGarnish = _menuReader.AllGarnishes[selectedGarnishIndex];
            burger.Garnishes.Add(selectedGarnish);

            Console.WriteLine($"Garnish '{selectedGarnish}' added to the burger.");
        }

        // Method to remove garnish from a burger
        private void RemoveGarnish(Burger burger)
        {
            if (burger.Garnishes.Count == 0)
            {
                Console.WriteLine("The burger has no garnishes to remove.");
                return;
            }

            Console.WriteLine("Current Garnishes:");
            for (int i = 0; i < burger.Garnishes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {burger.Garnishes[i]}");
            }

            Console.WriteLine("Enter the number of the garnish you want to remove:");
            int garnishIndexToRemove = ConsoleHelpers.GetIntegerInRange(1, burger.Garnishes.Count, "Enter the garnish number to remove:");

            string removedGarnish = burger.Garnishes[garnishIndexToRemove - 1];
            burger.Garnishes.RemoveAt(garnishIndexToRemove - 1);
            Console.WriteLine($"Garnish '{removedGarnish}' removed from the burger.");
        }

        // Method to display all orders and calculate total amount
        private void DisplayOrders()
        {
            decimal total = 0; // Initialize total amount
            const decimal deliveryCharge = 200; // Delivery charge if total is less than £20

            Console.WriteLine("All Orders:");
            foreach (var order in _orders)
            {
                Console.WriteLine(order.MenuText()); // Display order details

                if (order is Burger burger)
                {
                    total += burger.Price; // Add burger price to total
                }
                else if (order is Pizza pizza)
                {
                    total += pizza.Price; // Add pizza price to total

                    // Check if the pizza has toppings
                    if (pizza.Toppings.Count > 0)
                    {
                        // Add the price of each topping to the total
                        foreach (var topping in pizza.Toppings)
                        {
                            decimal toppingPrice = _menuReader.GetToppingPrice(topping);
                            total += toppingPrice;
                        }
                    }
                }
            }

            // Add delivery charge if applicable
            if (_delivery && total < 2000)
            {
                total += deliveryCharge;
                Console.WriteLine("A charge of £2 has been added for delivery.");
            }

            // Display free delivery message if total is over £20
            if (_delivery && total > 2000)
            {
                Console.WriteLine("Delivery is free!");
            }

            Console.WriteLine($"Total: £{total / 100:F2}"); // Display total amount
            SaveOrderToFile(); // Save order details to file
        }

        // Method to save order details to a file
        private void SaveOrderToFile()
        {
            string fileName = $"{_customerName}_Order.txt"; // Generate file name
            using (StreamWriter writer = new StreamWriter(fileName)) // Create StreamWriter object to write to file
            {
                writer.WriteLine($"Customer Name: {_customerName}"); // Write customer name to file
                writer.WriteLine("Orders:"); // Write orders header to file
                foreach (var order in _orders)
                {
                    writer.WriteLine(order.MenuText()); // Write each order to file
                }
            }
            Console.WriteLine($"Order details saved to {fileName}"); // Display success message
        }
    }
}

