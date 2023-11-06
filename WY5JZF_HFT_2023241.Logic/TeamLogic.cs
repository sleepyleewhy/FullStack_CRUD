using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Logic
{
    public class TeamLogic
    {
        IRepository<Team> repo;
        public TeamLogic(IRepository<Team> repo)
        {
            this.repo = repo;
        }
        public void Create(Team item)
        {

        }
    }
}
