using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Linq;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Repository
{
    public class NBADBContext : DbContext
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public NBADBContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                // string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf"";Integrated Security=True;MultipleActiveResultSets=true";
                builder.UseInMemoryDatabase("Basketball").UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(team => team.HasOne(team => team.Division)
            .WithMany(division => division.Teams).HasForeignKey(team => team.DivisionID)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Player>(player => player.HasOne(player => player.Team)
            .WithMany(team => team.Players).HasForeignKey(player => player.TeamID)
                           .OnDelete(DeleteBehavior.Cascade));


            Division Atlantic = new Division { DivisionId = 1, DivisionName = "Atlantic", Population = 42415680 };
            Division Central = new Division { DivisionId = 2, DivisionName = "Central", Population = 47747255 };
            Division Southeast = new Division { DivisionId = 3, DivisionName = "Southeast", Population = 41858760 };
            Division Northwest = new Division { DivisionId = 4, DivisionName = "Northwest", Population = 10164783 };
            Division Pacific = new Division { DivisionId = 5, DivisionName = "Pacific", Population = 45460491 };
            Division Southwest = new Division { DivisionId = 6, DivisionName = "Southwest", Population = 42376257 };
            Team[] teams = new Team[]
            {
                new Team { TeamId = 1, TeamName = "Boston Celtics", DivisionID = 1, FanCount = 100000 },
            new Team { TeamId = 2, TeamName = "Brooklyn Nets", DivisionID = 1, FanCount = 120000 },
            new Team { TeamId = 3, TeamName = "New York Knicks", DivisionID = 1, FanCount = 95000 },
            new Team { TeamId = 4, TeamName = "Philadelphia 76ers", DivisionID = 1, FanCount = 110000 },
            new Team { TeamId = 5, TeamName = "Toronto Raptors", DivisionID = 1, FanCount = 105000 },

            new Team { TeamId = 6, TeamName = "Chicago Bulls", DivisionID = 2, FanCount = 90000 },
            new Team { TeamId = 7, TeamName = "Cleveland Cavaliers", DivisionID = 2, FanCount = 85000 },
            new Team { TeamId = 8, TeamName = "Detroit Pistons", DivisionID = 2, FanCount = 80000 },
            new Team { TeamId = 9, TeamName = "Indiana Pacers", DivisionID = 2, FanCount = 95000 },
            new Team { TeamId = 10, TeamName = "Milwaukee Bucks", DivisionID = 2, FanCount = 100000 },

            new Team { TeamId = 11, TeamName = "Atlanta Hawks", DivisionID = 3, FanCount = 85000 },
            new Team { TeamId = 12, TeamName = "Charlotte Hornets", DivisionID = 3, FanCount = 75000 },
            new Team { TeamId = 13, TeamName = "Miami Heat", DivisionID = 3, FanCount = 110000 },
            new Team { TeamId = 14, TeamName = "Orlando Magic", DivisionID = 3, FanCount = 70000 },
            new Team { TeamId = 15, TeamName = "Washington Wizards", DivisionID = 3, FanCount = 90000 },

            new Team { TeamId = 16, TeamName = "Denver Nuggets", DivisionID = 4, FanCount = 85000 },
            new Team { TeamId = 17, TeamName = "Minnesota Timberwolves", DivisionID = 4, FanCount = 80000 },
            new Team { TeamId = 18, TeamName = "Oklahoma City Thunder", DivisionID = 4, FanCount = 75000 },
            new Team { TeamId = 19, TeamName = "Portland Trail Blazers", DivisionID = 4, FanCount = 90000 },
            new Team { TeamId = 20, TeamName = "Utah Jazz", DivisionID = 4, FanCount = 95000 },

            new Team { TeamId = 21, TeamName = "Golden State Warriors", DivisionID = 5, FanCount = 120000 },
            new Team { TeamId = 22, TeamName = "Los Angeles Clippers", DivisionID = 5, FanCount = 110000 },
            new Team { TeamId = 23, TeamName = "Los Angeles Lakers", DivisionID = 5, FanCount = 130000 },
            new Team { TeamId = 24, TeamName = "Phoenix Suns", DivisionID = 5, FanCount = 100000 },
            new Team { TeamId = 25, TeamName = "Sacramento Kings", DivisionID = 5, FanCount = 95000 },

            new Team { TeamId = 26, TeamName = "Dallas Mavericks", DivisionID = 6, FanCount = 90000 },
            new Team { TeamId = 27, TeamName = "Houston Rockets", DivisionID = 6, FanCount = 100000 },
            new Team { TeamId = 28, TeamName = "Memphis Grizzlies", DivisionID = 6, FanCount = 80000 },
            new Team { TeamId = 29, TeamName = "New Orleans Pelicans", DivisionID = 6, FanCount = 85000 },
            new Team { TeamId = 30, TeamName = "San Antonio Spurs", DivisionID = 6, FanCount = 95000 },
            };
            


            Player St_Curry = new Player { PlayerId = 1, PlayerName = "Stephen Curry", Position = 1, TeamID = 21, AvgPoints = 32.4, Salary = 60000 };
            Player Kl_Thompson = new Player { PlayerId = 2, PlayerName = "Klay Thompson", Position = 2, TeamID = 21, AvgPoints = 23.2, Salary = 40000 };
            Player Dr_Green = new Player { PlayerId = 3, PlayerName = "Draymond Green", Position = 4, TeamID = 21, AvgPoints = 12.3, Salary = 30000 };
            Player Lb_James = new Player { PlayerId = 4, PlayerName = "Lebron James", Position = 3, TeamID = 23, AvgPoints = 30, Salary = 30000 };
            Player An_Davis = new Player { PlayerId = 5, PlayerName = "Anthony Davis", Position = 5, TeamID = 23, AvgPoints = 15.7, Salary = 20000 };
            Player Gi_Ante = new Player { PlayerId = 6, PlayerName = "Giannis Antetokounmpo", Position = 4, TeamID = 10, AvgPoints = 24.5, Salary = 70000 };
            Player Kr_Middlet = new Player { PlayerId = 7, PlayerName = "Khris Middleton", Position = 3, TeamID = 10, AvgPoints = 21.1, Salary = 10000 };
            Player Dw_Wade = new Player { PlayerId = 8, PlayerName = "Dwayne Wade", Position = 2, TeamID = 13, AvgPoints = 12.3, Salary = 5000 };
            Player Js_Tatum = new Player { PlayerId = 9, PlayerName = "Jayson Tatum", Position = 3, TeamID = 1, AvgPoints = 22.4, Salary = 65000 };
            Player Zc_Lavine = new Player { PlayerId = 10, PlayerName = "Zach LaVine", Position = 1, TeamID = 6, AvgPoints = 26.4, Salary = 50000 };
            Player KD_Durant = new Player { PlayerId = 11, PlayerName = "Kevin Durant", Position = 3, TeamID = 24, AvgPoints = 35.3, Salary = 80000 };
            Player Ky_Irving = new Player { PlayerId = 12, PlayerName = "Kyrie Irving", Position = 1, TeamID = 2, AvgPoints = 26.9, Salary = 37000 };
            Player JHarden = new Player { PlayerId = 13, PlayerName = "James Harden", Position = 2, TeamID = 2, AvgPoints = 25.2, Salary = 40000 };
            Player RWestbrook = new Player { PlayerId = 14, PlayerName = "Russell Westbrook", Position = 1, TeamID = 2, AvgPoints = 27.2, Salary = 38000 };
            Player KLeonard = new Player { PlayerId = 15, PlayerName = "Kawhi Leonard", Position = 3, TeamID = 22, AvgPoints = 27.1, Salary = 36000 };
            Player PGeorge = new Player { PlayerId = 16, PlayerName = "Paul George", Position = 4, TeamID = 22, AvgPoints = 23.7, Salary = 34000 };
            Player DLillard = new Player { PlayerId = 17, PlayerName = "Damian Lillard", Position = 1, TeamID = 19, AvgPoints = 28.8, Salary = 38000 };
            Player CMcCollum = new Player { PlayerId = 18, PlayerName = "CJ McCollum", Position = 2, TeamID = 19, AvgPoints = 22.2, Salary = 30000 };
            Player LDoncic = new Player { PlayerId = 19, PlayerName = "Luka Doncic", Position = 1, TeamID = 17, AvgPoints = 27.7, Salary = 32000 };
            Player KPorzingis = new Player { PlayerId = 20, PlayerName = "Kristaps Porzingis", Position = 3, TeamID = 17, AvgPoints = 20.4, Salary = 30000 };
            Player BBeal = new Player { PlayerId = 21, PlayerName = "Bradley Beal", Position = 2, TeamID = 15, AvgPoints = 31.3, Salary = 35000 };
            Player DBooker = new Player { PlayerId = 22, PlayerName = "Devin Booker", Position = 1, TeamID = 25, AvgPoints = 27.6, Salary = 37000 };
            Player CPaul = new Player { PlayerId = 23, PlayerName = "Chris Paul", Position = 2, TeamID = 11, AvgPoints = 17.6, Salary = 30000 };
            Player DDeRozan = new Player { PlayerId = 24, PlayerName = "DeMar DeRozan", Position = 3, TeamID = 12, AvgPoints = 21.6, Salary = 32000 };
            Player LAldridge = new Player { PlayerId = 25, PlayerName = "LaMarcus Aldridge", Position = 4, TeamID = 12, AvgPoints = 19.1, Salary = 29000 };
            Player JButler = new Player { PlayerId = 26, PlayerName = "Jimmy Butler", Position = 5, TeamID = 3, AvgPoints = 21.5, Salary = 34000 };
            Player KWalker = new Player { PlayerId = 27, PlayerName = "Kemba Walker", Position = 1, TeamID = 5, AvgPoints = 19.3, Salary = 33000 };
            Player JEmbiid = new Player { PlayerId = 28, PlayerName = "Joel Embiid", Position = 5, TeamID = 4, AvgPoints = 26.7, Salary = 36000 };
            Player BSimmons = new Player { PlayerId = 29, PlayerName = "Ben Simmons", Position = 2, TeamID = 4, AvgPoints = 16.4, Salary = 31000 };
            Player TYoung = new Player { PlayerId = 30, PlayerName = "Trae Young", Position = 1, TeamID = 11, AvgPoints = 28.5, Salary = 35000 };
            Player RGoBear = new Player { PlayerId = 32, PlayerName = "Rudy Gobert", Position = 5, TeamID = 18, AvgPoints = 14.3, Salary = 28000 };
            Player DRussell = new Player { PlayerId = 33, PlayerName = "D'Angelo Russell", Position = 1, TeamID = 26, AvgPoints = 19.0, Salary = 29000 };
            Player KNunn = new Player { PlayerId = 34, PlayerName = "Kendrick Nunn", Position = 1, TeamID = 13, AvgPoints = 15.3, Salary = 27000 };
            Player PJWashington = new Player { PlayerId = 35, PlayerName = "PJ Washington", Position = 4, TeamID = 12, AvgPoints = 12.9, Salary = 26000 };
            Player JBridges = new Player { PlayerId = 36, PlayerName = "Miles Bridges", Position = 3, TeamID = 12, AvgPoints = 10.1, Salary = 25000 };
            Player JBarnes = new Player { PlayerId = 37, PlayerName = "Harrison Barnes", Position = 4, TeamID = 25, AvgPoints = 16.8, Salary = 30000 };
            Player DMitchell = new Player { PlayerId = 38, PlayerName = "Donovan Mitchell", Position = 2, TeamID = 24, AvgPoints = 24.0, Salary = 31000 };
            Player MHarris = new Player { PlayerId = 39, PlayerName = "Joe Harris", Position = 2, TeamID = 2, AvgPoints = 14.8, Salary = 26000 };
            Player JLamb = new Player { PlayerId = 40, PlayerName = "Jeremy Lamb", Position = 3, TeamID = 7, AvgPoints = 11.2, Salary = 24000 };
            Player DRose = new Player { PlayerId = 41, PlayerName = "Derrick Rose", Position = 1, TeamID = 8, AvgPoints = 18.1, Salary = 28000 };
            Player TFerguson = new Player { PlayerId = 42, PlayerName = "Terrance Ferguson", Position = 2, TeamID = 18, AvgPoints = 7.2, Salary = 23000 };
            Player CDinwiddie = new Player { PlayerId = 43, PlayerName = "Spencer Dinwiddie", Position = 1, TeamID = 2, AvgPoints = 20.6, Salary = 29000 };
            Player THardaway = new Player { PlayerId = 44, PlayerName = "Tim Hardaway Jr.", Position = 2, TeamID = 25, AvgPoints = 15.5, Salary = 25000 };
            Player MPorzingis = new Player { PlayerId = 45, PlayerName = "Maxi Kleber", Position = 4, TeamID = 26, AvgPoints = 7.5, Salary = 22000 };
            Player OAnunoby = new Player { PlayerId = 46, PlayerName = "OG Anunoby", Position = 3, TeamID = 5, AvgPoints = 10.6, Salary = 24000 };
            Player PSiakam = new Player { PlayerId = 47, PlayerName = "Pascal Siakam", Position = 4, TeamID = 5, AvgPoints = 21.4, Salary = 29000 };
            Player FVleet = new Player { PlayerId = 48, PlayerName = "Fred VanVleet", Position = 1, TeamID = 5, AvgPoints = 17.6, Salary = 26000 };
            Player JKidd = new Player { PlayerId = 49, PlayerName = "Jalen Brunson", Position = 1, TeamID = 26, AvgPoints = 9.3, Salary = 22000 };
            Player KMoon = new Player { PlayerId = 50, PlayerName = "Kevin Porter Jr.", Position = 2, TeamID = 27, AvgPoints = 11.4, Salary = 24000 };
            Player ZWill = new Player { PlayerId = 51, PlayerName = "Zion Williamson", Position = 4, TeamID = 29, AvgPoints = 25.7, Salary = 31000 };
            Player BLopez = new Player { PlayerId = 52, PlayerName = "Brook Lopez", Position = 5, TeamID = 10, AvgPoints = 12.3, Salary = 27000 };
            Player EBledsoe = new Player { PlayerId = 53, PlayerName = "Eric Bledsoe", Position = 1, TeamID = 10, AvgPoints = 14.9, Salary = 28000 };
            Player JHoliday = new Player { PlayerId = 54, PlayerName = "Jrue Holiday", Position = 2, TeamID = 10, AvgPoints = 17.6, Salary = 30000 };
            Player DWright = new Player { PlayerId = 55, PlayerName = "Delon Wright", Position = 1, TeamID = 6, AvgPoints = 9.4, Salary = 25000 };
            Player DWrightJr = new Player { PlayerId = 56, PlayerName = "Dorell Wright", Position = 3, TeamID = 30, AvgPoints = 7.8, Salary = 23000 };
            Player JBol = new Player { PlayerId = 57, PlayerName = "Bol Bol", Position = 5, TeamID = 30, AvgPoints = 8.2, Salary = 22000 };
            Player KAnthony = new Player { PlayerId = 58, PlayerName = "Carmelo Anthony", Position = 4, TeamID = 19, AvgPoints = 14.4, Salary = 28000 };
            Player AHoliday = new Player { PlayerId = 59, PlayerName = "Aaron Holiday", Position = 1, TeamID = 12, AvgPoints = 9.5, Salary = 24000 };
            Player TJMcConnell = new Player { PlayerId = 60, PlayerName = "T.J. McConnell", Position = 2, TeamID = 4, AvgPoints = 6.7, Salary = 22000 };
            Player MBridges = new Player { PlayerId = 61, PlayerName = "Mikal Bridges", Position = 3, TeamID = 24, AvgPoints = 12.7, Salary = 26000 };
            Player AJHampton = new Player { PlayerId = 62, PlayerName = "RJ Hampton", Position = 1, TeamID = 18, AvgPoints = 7.8, Salary = 23000 };
            Player JJohnson = new Player { PlayerId = 63, PlayerName = "James Johnson", Position = 4, TeamID = 26, AvgPoints = 8.3, Salary = 22000 };
            Player CJohnson = new Player { PlayerId = 64, PlayerName = "Cameron Johnson", Position = 3, TeamID = 22, AvgPoints = 9.7, Salary = 24000 };
            Player QCook = new Player { PlayerId = 65, PlayerName = "Quinn Cook", Position = 2, TeamID = 29, AvgPoints = 5.5, Salary = 21000 };
            Player ABrooks = new Player { PlayerId = 66, PlayerName = "Dillon Brooks", Position = 3, TeamID = 28, AvgPoints = 17.2, Salary = 27000 };
            Player KAnderson = new Player { PlayerId = 67, PlayerName = "Kyle Anderson", Position = 4, TeamID = 28, AvgPoints = 12.4, Salary = 25000 };
            Player ERogers = new Player { PlayerId = 68, PlayerName = "Elijah Hughes", Position = 2, TeamID = 15, AvgPoints = 6.8, Salary = 21000 };
            Player PDozier = new Player { PlayerId = 69, PlayerName = "PJ Dozier", Position = 1, TeamID = 17, AvgPoints = 7.3, Salary = 22000 };
            Player RMillsap = new Player { PlayerId = 70, PlayerName = "Paul Millsap", Position = 4, TeamID = 30, AvgPoints = 8.7, Salary = 23000 };

            Player[] players = new Player[]
{
    St_Curry,
    Kl_Thompson,
    Dr_Green,
    Lb_James,
    An_Davis,
    Gi_Ante,
    Kr_Middlet,
    Dw_Wade,
    Js_Tatum,
    Zc_Lavine,
    KD_Durant,
    Ky_Irving,
    JHarden,
    RWestbrook,
    KLeonard,
    PGeorge,
    DLillard,
    CMcCollum,
    LDoncic,
    KPorzingis,
    BBeal,
    DBooker,
    CPaul,
    DDeRozan,
    LAldridge,
    JButler,
    KWalker,
    JEmbiid,
    BSimmons,
    TYoung,
    RGoBear,
    DRussell,
    KNunn,
    PJWashington,
    JBridges,
    JBarnes,
    DMitchell,
    MHarris,
    JLamb,
    DRose,
    TFerguson,
    CDinwiddie,
    THardaway,
    MPorzingis,
    OAnunoby,
    PSiakam,
    FVleet,
    JKidd,
    KMoon,
    ZWill,
    BLopez,
    EBledsoe,
    JHoliday,
    DWright,
    DWrightJr,
    JBol,
    KAnthony,
    AHoliday,
    TJMcConnell,
    MBridges,
    AJHampton,
    JJohnson,
    CJohnson,
    QCook,
    ABrooks,
    KAnderson,
    ERogers,
    PDozier,
    RMillsap,new Player { PlayerId = 71, PlayerName = "Brandon Ingram", Position = 3, TeamID = 23, AvgPoints = 26.2, Salary = 38000 },
    // 32nd player
    new Player { PlayerId = 72, PlayerName = "De'Aaron Fox", Position = 1, TeamID = 9, AvgPoints = 24.6, Salary = 35000 },
    // 33rd player
    new Player { PlayerId = 73, PlayerName = "John Collins", Position = 4, TeamID = 8, AvgPoints = 17.9, Salary = 28000 },
    // 34th player
    new Player { PlayerId = 74, PlayerName = "Nikola Vucevic", Position = 5, TeamID = 14, AvgPoints = 22.5, Salary = 32000 },
    // 35th player
    new Player { PlayerId = 75, PlayerName = "Brandon Clarke", Position = 4, TeamID = 14, AvgPoints = 14.2, Salary = 26000 },
    // 36th player
    new Player { PlayerId = 76, PlayerName = "Jaren Jackson Jr.", Position = 4, TeamID = 14, AvgPoints = 15.6, Salary = 27000 },
    // 37th player
    new Player { PlayerId = 77, PlayerName = "Dillon Brooks", Position = 2, TeamID = 14, AvgPoints = 15.7, Salary = 25000 },
    // 38th player
    new Player { PlayerId = 78, PlayerName = "Shai Gilgeous-Alexander", Position = 1, TeamID = 7, AvgPoints = 23.6, Salary = 34000 },
    // 39th player
    new Player { PlayerId = 79, PlayerName = "Zach Collins", Position = 4, TeamID = 8, AvgPoints = 9.0, Salary = 23000 },
    // 40th player
    new Player { PlayerId = 80, PlayerName = "Hassan Whiteside", Position = 5, TeamID = 8, AvgPoints = 11.8, Salary = 27000 },
    // 41st player
    new Player { PlayerId = 81, PlayerName = "Montrezl Harrell", Position = 5, TeamID = 16, AvgPoints = 18.6, Salary = 29000 },
    // 42nd player
    new Player { PlayerId = 82, PlayerName = "Lou Williams", Position = 2, TeamID = 16, AvgPoints = 14.4, Salary = 26000 },
    // 43rd player
    new Player { PlayerId = 83, PlayerName = "Christian Wood", Position = 4, TeamID = 20, AvgPoints = 20.9, Salary = 31000 },
    // 44th player
    new Player { PlayerId = 84, PlayerName = "John Wall", Position = 1, TeamID = 20, AvgPoints = 21.7, Salary = 32000 },
    // 45th player
    new Player { PlayerId = 85, PlayerName = "Eric Gordon", Position = 2, TeamID = 20, AvgPoints = 15.6, Salary = 27000 },
    // 46th player
    new Player { PlayerId = 86, PlayerName = "Buddy Hield", Position = 2, TeamID = 9, AvgPoints = 19.2, Salary = 28000 },
    // 47th player
    new Player { PlayerId = 87, PlayerName = "Richaun Holmes", Position = 5, TeamID = 9, AvgPoints = 12.3, Salary = 24000 },
    // 48th player
    new Player { PlayerId = 88, PlayerName = "Marvin Bagley III", Position = 4, TeamID = 9, AvgPoints = 14.9, Salary = 26000 },
    // 49th player
    new Player { PlayerId = 89, PlayerName = "Andrew Wiggins", Position = 3, TeamID = 27, AvgPoints = 18.1, Salary = 29000 },
    // 50th player
    new Player { PlayerId = 90, PlayerName = "Jordan Poole", Position = 1, TeamID = 27, AvgPoints = 8.7, Salary = 23000 },
    // 51st player
    new Player { PlayerId = 91, PlayerName = "James Wiseman", Position = 5, TeamID = 27, AvgPoints = 9.4, Salary = 24000 },
    // 52nd player
    new Player { PlayerId = 92, PlayerName = "John Wall", Position = 1, TeamID = 26, AvgPoints = 21.7, Salary = 32000 },
    // 53rd player
    new Player { PlayerId = 93, PlayerName = "Kelly Olynyk", Position = 4, TeamID = 26, AvgPoints = 10.1, Salary = 25000 },
    // 54th player
    new Player { PlayerId = 94, PlayerName = "Jae Crowder", Position = 4, TeamID = 21, AvgPoints = 9.8, Salary = 23000 },
    // 55th player
    new Player { PlayerId = 95, PlayerName = "Victor Oladipo", Position = 2, TeamID = 26, AvgPoints = 18.8, Salary = 28000 },
    // 56th player
    new Player { PlayerId = 96, PlayerName = "Danilo Gallinari", Position = 4, TeamID = 9, AvgPoints = 14.1, Salary = 26000 },
    // 57th player
    new Player { PlayerId = 97, PlayerName = "Bogdan Bogdanovic", Position = 2, TeamID = 9, AvgPoints = 16.3, Salary = 27000 },
    // 58th player
    new Player { PlayerId = 98, PlayerName = "Collin Sexton", Position = 1, TeamID = 8, AvgPoints = 24.3, Salary = 33000 },
    // 59th player
    new Player { PlayerId = 99, PlayerName = "Darius Garland", Position = 1, TeamID = 8, AvgPoints = 16.4, Salary = 26000 },
    // 60th player
    new Player { PlayerId = 100, PlayerName = "Ricky Rubio", Position = 1, TeamID = 16, AvgPoints = 10.2, Salary = 27000 },
    new Player { PlayerId = 101, PlayerName = "Dejounte Murray", Position = 1, TeamID = 20, AvgPoints = 14.5, Salary = 28000 },
    new Player { PlayerId = 102, PlayerName = "Jamal Murray", Position = 1, TeamID = 14, AvgPoints = 19.8, Salary = 31000 },
    new Player { PlayerId = 103, PlayerName = "Michael Porter Jr.", Position = 3, TeamID = 14, AvgPoints = 13.7, Salary = 25000 },
    new Player { PlayerId = 104, PlayerName = "Buddy Hield", Position = 2, TeamID = 16, AvgPoints = 17.9, Salary = 29000 },
    new Player { PlayerId = 105, PlayerName = "Deandre Ayton", Position = 5, TeamID = 16, AvgPoints = 18.2, Salary = 32000 },
    new Player { PlayerId = 106, PlayerName = "Ja Morant", Position = 1, TeamID = 7, AvgPoints = 20.1, Salary = 35000 },
    new Player { PlayerId = 107, PlayerName = "Jaren Jackson Jr.", Position = 4, TeamID = 7, AvgPoints = 14.6, Salary = 27000 },
    new Player { PlayerId = 108, PlayerName = "Zach Collins", Position = 4, TeamID = 21, AvgPoints = 9.8, Salary = 24000 },
    new Player { PlayerId = 109, PlayerName = "Hassan Whiteside", Position = 5, TeamID = 21, AvgPoints = 11.5, Salary = 26000 },
    new Player { PlayerId = 110, PlayerName = "Evan Fournier", Position = 2, TeamID = 9, AvgPoints = 16.7, Salary = 28000 },
    new Player { PlayerId = 111, PlayerName = "Markelle Fultz", Position = 1, TeamID = 9, AvgPoints = 12.3, Salary = 25000 },
    new Player { PlayerId = 112, PlayerName = "Jonathan Isaac", Position = 4, TeamID = 9, AvgPoints = 9.6, Salary = 24000 },
    new Player { PlayerId = 113, PlayerName = "Nikola Vucevic", Position = 5, TeamID = 9, AvgPoints = 22.8, Salary = 33000 },
    new Player { PlayerId = 114, PlayerName = "De'Aaron Fox", Position = 1, TeamID = 27, AvgPoints = 24.3, Salary = 34000 },
    new Player { PlayerId = 115, PlayerName = "Bogdan Bogdanovic", Position = 2, TeamID = 27, AvgPoints = 15.2, Salary = 26000 },
    new Player { PlayerId = 116, PlayerName = "Harrison Barnes", Position = 3, TeamID = 27, AvgPoints = 14.6, Salary = 27000 },
    new Player { PlayerId = 117, PlayerName = "Marvin Bagley III", Position = 4, TeamID = 27, AvgPoints = 12.7, Salary = 25000 },
    new Player { PlayerId = 118, PlayerName = "Richaun Holmes", Position = 5, TeamID = 27, AvgPoints = 10.9, Salary = 24000 },
    new Player { PlayerId = 119, PlayerName = "Malcolm Brogdon", Position = 2, TeamID = 8, AvgPoints = 18.5, Salary = 29000 },
    new Player { PlayerId = 120, PlayerName = "Domantas Sabonis", Position = 4, TeamID = 8, AvgPoints = 20.3, Salary = 32000 },
    new Player { PlayerId = 121, PlayerName = "Myles Turner", Position = 5, TeamID = 8, AvgPoints = 12.4, Salary = 26000 },
    new Player { PlayerId = 122, PlayerName = "Jerami Grant", Position = 3, TeamID = 18, AvgPoints = 19.4, Salary = 30000 },
    new Player { PlayerId = 123, PlayerName = "Coby White", Position = 1, TeamID = 30, AvgPoints = 15.1, Salary = 28000 },
    new Player { PlayerId = 124, PlayerName = "Patrick Williams", Position = 4, TeamID = 30, AvgPoints = 9.8, Salary = 24000 },
    new Player { PlayerId = 125, PlayerName = "Obi Toppin", Position = 4, TeamID = 1, AvgPoints = 8.6, Salary = 23000 },
    new Player { PlayerId = 126, PlayerName = "Immanuel Quickley", Position = 1, TeamID = 1, AvgPoints = 11.9, Salary = 25000 },
    new Player { PlayerId = 127, PlayerName = "RJ Barrett", Position = 2, TeamID = 1, AvgPoints = 18.5, Salary = 29000 },
    new Player { PlayerId = 128, PlayerName = "Mitchell Robinson", Position = 5, TeamID = 1, AvgPoints = 9.8, Salary = 24000 },
    new Player { PlayerId = 129, PlayerName = "Derrick White", Position = 1, TeamID = 20, AvgPoints = 12.6, Salary = 26000 }
        };







            modelBuilder.Entity<Division>().HasData(new Division[]
            {
                Pacific, Atlantic, Central, Southeast, Northwest, Southwest
            });
            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<Player>().HasData(players);
        }
    }
}
