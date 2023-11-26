using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Repository
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(NBADBContext ctx) : base(ctx)
        {
        }
        public override Player Read(int id)
        {
            return ctx.Players.First(x => x.PlayerId == id);
        }
        public override void Update(Player item)
        {
            var old = Read(item.PlayerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
