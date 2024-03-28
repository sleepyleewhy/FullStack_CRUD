using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Logic;
using WY5JZF_HFT_2023241.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WY5JZF_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {


        IDivisionLogic logic;

        public DivisionController(IDivisionLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<Division> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public Division  Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<PlayerController>
        [HttpPost]
        public void Create([FromBody] Division value)
        {
            this.logic.Create(value);
        }

        // PUT api/<PlayerController>/5
        [HttpPut]
        public void Update([FromBody] Division value)
        {
           this.logic.Update(value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
