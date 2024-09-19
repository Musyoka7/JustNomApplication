// See https://aka.ms/new-console-template for more information
using JustNomApplication.Menu;
using JustNomApplication.Restuarant;
using static JustNomApplication.Menu.ReadMenu;

//using static JustNomApplication.Restuarant;

Console.WriteLine("Hello, World!");
string filePath = "recipe.txt";
MenuReader menuReader = new MenuReader();
menuReader.ReadMenuFromFile(filePath);

Order order = new Order(menuReader);
order.Select();
Console.ReadLine();




