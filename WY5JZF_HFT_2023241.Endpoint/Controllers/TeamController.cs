using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Endpoint.Services;
using WY5JZF_HFT_2023241.Logic;
using WY5JZF_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WY5JZF_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        IHubContext<SignalRHub> hub;
        ITeamLogic logic;

        public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Team Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<PlayerController>
        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("TeamCreated", value);
        }

        // PUT api/<PlayerController>/5
        [HttpPut]
        public void Update([FromBody] Team value)
        {
           this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TeamUpdated", value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Team teamToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
        }
    }
}
