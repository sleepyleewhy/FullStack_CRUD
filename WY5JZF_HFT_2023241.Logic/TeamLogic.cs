using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Logic
{
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> repo;
        public TeamLogic(IRepository<Team> repo)
        {
            this.repo = repo;
        }
        public void Create(Team item)
        {
            if (item.TeamName.Length > 120)
            {
                throw new ArgumentException("Team name too long!");
            }
            else
            {
                repo.Create(item);
            }
        }
        public Team Read(int id)
        {
            var item = repo.Read(id);
            if (item == null)
            {
                throw new ArgumentException("Team doesn't exist!");
            }
            else
            {
                return item;
            }
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public IEnumerable<Team> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Team item)
        {
            repo.Update(item);
        }

        public IEnumerable<Player> AllPosPlayerInTeam(int positionID, int teamID)
        {
            return repo.ReadAll().Where(t => t.TeamId == teamID).SelectMany(t => t.Players).Where(t => t.Position == positionID);

        }
        public double AvgPointsPerTeam(int teamID)
        {
            return repo.Read(teamID).Players.Sum(t => t.AvgPoints);

        }
    }
}
