using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Threading;
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
                .Add("Read", () => Read("Division"))
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Read", () => Read("Team"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Read", () => Read("Player"))
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
                    Console.WriteLine(division.DivisionName + " " + division.DivisionId);
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
                Console.Write("Division Name: ");
                string divname = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Population: ");
                int pop = int.Parse(Console.ReadLine());
                Division div = new Division()
                {
                    DivisionName = divname,
                    Population = pop
                };
                rest.Post(div, "division");
                Console.WriteLine("Division created.");
            }
            else if (entity == "Team")
            {
                Console.Write("Team name: ");
                string teamname = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Fan count: ");
                int fan = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Division id: ");
                int divid = int.Parse(Console.ReadLine());

                Team team = new Team()
                {
                    TeamName = teamname,
                    FanCount = fan,
                    DivisionID = divid
                };
                rest.Post(team, "team");
                Console.WriteLine("Team created.");
            }
            else if (entity == "Player")
            {
                Console.Write("Player name: ");
                string name = Console.ReadLine();                
                Console.Write("Position: ");
                int pos = int.Parse(Console.ReadLine());                
                Console.Write("Average points: ");
                double poi = double.Parse(Console.ReadLine());               
                Console.Write("Salary: ");
                int sal = int.Parse(Console.ReadLine());
                Console.Write("Team id: ");
                int teamid = int.Parse(Console.ReadLine());
                Player player = new Player()
                {
                    AvgPoints = poi,
                    PlayerName = name,
                    Position = pos,
                    Salary = sal,
                    TeamID = teamid
                };
                rest.Post(player, "player");
                Console.WriteLine("Player created.");
            }
            Console.ReadLine();
        }
        static void Read(string entity)
        {
            if (entity == "Division")
            {
                Console.Write("ID of Division: ");
                int divid = int.Parse(Console.ReadLine());

                Division div = rest.Get<Division>(divid, "division");
                Console.WriteLine("Division name: "+ div.DivisionName);
                Console.WriteLine("Population: "+ div.Population);
                Console.Write("Teams: ");
                foreach (Team item in div.Teams)
                {
                    Console.Write(item.TeamName + " ");
                }


            }
            else if (entity == "Team")
            {
                Console.Write("ID of Team: ");
                int id = int.Parse(Console.ReadLine());
                Team team = rest.Get<Team>(id, "team");

                Console.WriteLine("Team name: "+ team.TeamName);
                Console.WriteLine("Fan count: "+ team.FanCount);
                Console.WriteLine("Division: " + team.Division.DivisionName);
                Console.Write("Players: ");
                foreach (Player item in team.Players)
                {
                    Console.Write(item.PlayerName + " ");
                }
            }
            else if (entity == "Player")
            {
                Console.Write("ID of Player: ");
                int id = int.Parse(Console.ReadLine());

                Player player = rest.Get<Player>(id, "player");
                Console.WriteLine("Player name: " + player.PlayerName);
                Console.WriteLine("Player position: " + player.Position);
                Console.WriteLine("Average points: " + player.AvgPoints);
                Console.WriteLine("Salary: " + player.Salary);
                Console.WriteLine("Team: " + player.Team.TeamName);

            }
            Console.ReadLine();
        }


        
    }
}
