using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Repository
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(NBADBContext ctx) : base(ctx)
        {
        }
        public override Team Read(int id)
        {
            return ctx.Teams.First(x => x.TeamId == id);
        }
        public override void Update(Team item)
        {
            var old = Read(item.TeamId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
