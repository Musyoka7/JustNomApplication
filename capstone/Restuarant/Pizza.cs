using JustNomApplication.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNomApplication.Restuarant
{
    //* Each pizza has a name, price,
    //and a list of toppings.
    //The class provides a constructor to initialize these properties,
    //and methods to select a pizza and display its information in a formatted menu text. Overall,
    //the Pizza class encapsulates the essential attributes and behaviors of a pizza menu item in the application.*//
    internal class Pizza : MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Toppings { get; set; } // Add Toppings property

        public Pizza(string name, decimal price, List<string> toppings)
        {
            Name = name;
            Price = price;
            Toppings = toppings;
        }

        public override void Select()
        {
            Console.WriteLine($"Selected Pizza: {Name}, Price: {Price}");
        }

        public override string MenuText()
        {
            return $"{Name} - £{Price / 100:F2}";
        }
    }
}
