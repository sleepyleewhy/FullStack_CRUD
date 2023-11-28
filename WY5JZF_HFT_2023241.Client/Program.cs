using ConsoleTools;
using System;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Client
{
    internal class Program
    {

        static RestService rest;
        static void Main(string[] args)
        {

            rest = new RestService("http://localhost:40930/", "division");
            var divisionSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Division"))
                .Add("Create", () => Create("Division"))
                .Add("Delete", () => Delete("Division"))
                .Add("Update", () => Update("Division"))
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
                List<Division> divisions = rest.Get<Division>("division");
                foreach (Division division in divisions)
                {
                    Console.WriteLine(division.DivisionName);
                }
            }
            else if (entity == "Team")
            {
                List<Team> teams = rest.Get<Team>("team");
                foreach (Team item in teams)
                {
                    Console.WriteLine(item.TeamName);
                }
            }
            else if (entity == "Player")
            {
                List<Player> players = rest.Get<Player>("player");
                foreach(Player player in players)
                {
                    Console.WriteLine(player.PlayerName);
                }
            }

            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity+ " update");
            if (entity == "Division")
            {
                rest.Put<Division>()
            }

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
