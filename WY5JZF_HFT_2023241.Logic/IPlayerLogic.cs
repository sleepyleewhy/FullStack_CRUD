using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Logic
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        IEnumerable<Player> ReadAll();
        void Update(Player item);
        Player Read(int id);

    }
}
