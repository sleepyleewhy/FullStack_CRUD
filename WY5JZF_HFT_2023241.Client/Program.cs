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
                Console.WriteLine("ID of Division : ");
                int id = int.Parse(Console.ReadLine());
                Division updatable = rest.Get<Division>(id, "division");
                Console.WriteLine("Name of property to update: ");
                string property = Console.ReadLine().ToLower();
                Console.WriteLine("Value: ");
                switch (property)
                {
                    case "divisionname":
                        updatable.DivisionName = Console.ReadLine();
                        break;
                    case "population":
                        updatable.Population = int.Parse(Console.ReadLine());
                        break;
                }
                rest.Put<Division>(updatable, "division");
                Console.WriteLine("Division updated.");
            }
            else if (entity == "Team")
            {
                Console.WriteLine("ID of Team : ");
                int id = int.Parse(Console.ReadLine());
                Team updatable = rest.Get<Team>(id, "team");
                Console.WriteLine("Name of property to update: ");
                string property = Console.ReadLine().ToLower();
                Console.WriteLine("Value: ");
                switch (property)
                {
                    case "teamname":
                        updatable.TeamName= Console.ReadLine();
                        break;
                    case "fancount":
                        updatable.FanCount= int.Parse(Console.ReadLine());
                        break;
                    case "divisionid":
                        updatable.DivisionID = int.Parse(Console.ReadLine());
                        break;
                }
                rest.Put<Team>(updatable, "team");
                Console.WriteLine("Team updated.");
            }
            if (entity == "Player")
            {
                Console.WriteLine("ID of Player : ");
                int id = int.Parse(Console.ReadLine());
                Player updatable = rest.Get<Player>(id, "player");
                Console.WriteLine("Name of property to update: ");
                string property = Console.ReadLine().ToLower();
                Console.WriteLine("Value: ");
                switch (property)
                {
                    case "playername":
                        updatable.PlayerName= Console.ReadLine();
                        break;
                    case "salary":
                        updatable.Salary= int.Parse(Console.ReadLine());
                        break;
                    case "teamid":
                        updatable.TeamID = int.Parse(Console.ReadLine());
                        break;
                    case "avgpoints":
                        updatable.AvgPoints= double.Parse(Console.ReadLine());
                        break;
                    case "position":                        
                        updatable.Position = int.Parse(Console.ReadLine());
                        break;
                }
                rest.Put<Player>(updatable, "player");
                Console.WriteLine("Division updated.");
            }

            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            if (entity == "Division")
            {
                Console.WriteLine("Id of Division : ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "division");
                Console.WriteLine("Division deleted.");
            }
            else if (entity == "Team")
            {
                Console.WriteLine("Id of Team : ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "team");
                Console.WriteLine("Team deleted.");
            }
            else if (entity == "Player")
            {
                Console.WriteLine("Id of Player: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "player");
                Console.WriteLine("Player deleted.");
            }

            Console.ReadLine();
        }
        static void Create(string entity)
        {
            if (entity == "Division")
            {
                var 
            }
            Console.ReadLine();
        }


        
    }
}
