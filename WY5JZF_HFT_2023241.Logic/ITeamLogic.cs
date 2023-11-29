using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Logic
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        IEnumerable<Team> ReadAll();
        void Update(Team item);
        Team Read(int id);
        IEnumerable<Player> AllPosPlayerInTeam(int positionID, int teamID);
        double AvgPointsPerTeam(int teamID);

    }
}
