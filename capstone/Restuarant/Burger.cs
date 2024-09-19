using JustNomApplication.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNomApplication.Restuarant
{
    //*. Each burger has a name, price,
    //and a list of garnishes along with their prices.
    //The class provides a constructor to initialize these properties,
    //and methods to select a burger and display its information in a formatted menu text.
    //Overall, the Burger class encapsulates the essential attributes and behaviors of a
    //burger menu item in the application.*//
    //**//
    internal class Burger : MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Garnishes { get; set; } // garnish property 
        public List<int> GarnishPrices { get; set; }
        public Burger(string name, decimal price, List<string> garnishes, List<int> garnishPrices)
        {
            Name = name;
            Price = price;
            Garnishes = garnishes;
            GarnishPrices = garnishPrices;
        }

        public override void Select()
        {
            Console.WriteLine($"Selected Burger: {Name}, Price: {Price}");
        }

        public override string MenuText()
        {
            return $"{Name} - £{Price / 100:F2}";
        }
    }
}
