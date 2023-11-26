using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Repository
{
    public class DivisionRepository : Repository<Division>, IRepository<Division>
    {
        public DivisionRepository(NBADBContext ctx) : base(ctx)
        {
        }
        public override Division Read(int id)
        {
            return ctx.Divisions.First(x => x.DivisionId == id);
        }
        public override void Update(Division item)
        {
            var old = Read(item.DivisionId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
