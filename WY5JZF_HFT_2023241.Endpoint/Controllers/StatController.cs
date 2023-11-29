using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Logic;

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
        [HttpGet("{}")]
        pu
    }
}
