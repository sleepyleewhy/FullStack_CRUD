using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Logic;
using WY5JZF_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WY5JZF_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IDivisionLogic divLogic;
        ITeamLogic teamLogic;
        IPlayerLogic playerLogic;

        public StatController(IDivisionLogic divLogic, ITeamLogic teamLogic, IPlayerLogic playerLogic)
        {
            this.divLogic = divLogic;
            this.teamLogic = teamLogic;
            this.playerLogic = playerLogic;
        }
        [HttpGet("{positionID}/{teamID}")]
        public IEnumerable<Player> AllPosPlayerInTeam(int positionID, int teamID)
        {
            return teamLogic.AllPosPlayerInTeam(positionID, teamID);
        }
        [HttpGet("{teamID}")]
        public double AvgPointsPerTeam(int teamID)
        {
            return teamLogic.AvgPointsPerTeam(teamID);
        }

        [HttpGet("{divisionID}")]
        public IEnumerable<Player> Top3PointsInDiv(int divisionID)
        {
            return divLogic.Top3PointsInDiv(divisionID);
        }

        [HttpGet("{divisionID}")]
        public int AllFansPerDivision(int divisionID)
        {
            return divLogic.AllFansPerDivision(divisionID);
        }
        [HttpGet("{divID}")]
        public Team TeamWithMostSalaryCostInDiv(int divID)
        {
            return divLogic.TeamWithMostSalaryCostInDiv(divID);
        }

    }
}
