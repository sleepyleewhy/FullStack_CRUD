using ConsoleTools;
using System;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var divisionSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Director"))
                .Add("Create", () => Create("Director"))
                .Add("Delete", () => Delete("Director"))
                .Add("Update", () => Update("Director"))
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Divisions", () => divisionSubMenu.Show())
                .Add("Teams", () => teamSubMenu.Show())
                .Add("Players", () => playerSubMenu.Show());


            menu.Show();
            
        }
        static void List(string entity)
        {
            if (entity == "Division")
            {

            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity+ " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity+ " delete");
            Console.ReadLine();
        }
        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }


        
    }
}
