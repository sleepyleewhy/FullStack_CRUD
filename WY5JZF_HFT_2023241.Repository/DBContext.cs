using Microsoft.EntityFrameworkCore;
using System;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Repository
{
    public class DBContext : DbContext
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public DBContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"";
                builder.UseSqlServer(conn);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(team => team.HasOne<Division>()
            .WithMany().HasForeignKey(team => team.DivisionID)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Player>(player => player.HasOne<Team>()
            .WithMany().HasForeignKey(player => player.TeamID)
                           .OnDelete(DeleteBehavior.Cascade));

            Division Pacific = new Division { DivisionId = 1, DivisionName = "Pacific", Population = 10000000 };
            Division Atlantic = new Division { DivisionId = 2, DivisionName = "Atlantic", Population = 20000000 };
            Division Central = new Division { DivisionId = 3, DivisionName = "Central", Population = 30000000 };
            Division Southeast = new Division { DivisionId = 4, DivisionName = "Southeast", Population = 40000000 };

            Team GS_Warriors = new Team { TeamId = 1, TeamName = "Golden State Warriors", DivisionID = 1 };
            Team LA_Lakers = new Team { TeamId = 2, TeamName = "LA Lakers", DivisionID = 1 };
            Team MIL_Bucks = new Team { TeamId = 3, TeamName = "Milwaukee Bucks", DivisionID = 3 };
            Team MIA_Heat = new Team { TeamId= 4, TeamName = "Miami Heat", DivisionID = 4 };
            Team BOS_Celtics =new Team { TeamId = 5, TeamName = "Boston Celtics", DivisionID = 2 };

            Player St_Curry = new Player { PlayerId = 1, PlayerName = "Stephen Curry", Position = 1, TeamID = 1 };
            Player Lb_James = new Player { PlayerId= 2, PlayerName = "Lebron James", Position = 3, TeamID = 2 };
            Player Gi_Ante = new Player { PlayerId = 3, PlayerName = "Giannis Antetokounmpo", Position = 4, TeamID = 3 };
            Player Dw_Wade = new Player { PlayerId = 4, PlayerName = "Dwayne Wade", Position = 2, TeamID = 4 };
            Player Js_Tatum = new Player { PlayerId = 5, PlayerName = "Jayson Tatum", Position = 3, TeamID = 5 };

            modelBuilder.Entity<Division>().HasData(new Division[]
            {
                Pacific, Atlantic, Central, Southeast
            });
            modelBuilder.Entity<Team>().HasData(new Team[]
            {
                GS_Warriors, LA_Lakers, MIL_Bucks, MIA_Heat, BOS_Celtics
            });
            modelBuilder.Entity<Player>().HasData(new Player[]
            {
                St_Curry, Lb_James, Gi_Ante, Dw_Wade, Js_Tatum
            });
        }
    }
}
