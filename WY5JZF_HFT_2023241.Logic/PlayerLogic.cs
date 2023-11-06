using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Logic
{
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> repo;
        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }
        public void Create(Player item)
        {
            if (item.PlayerName.Length > 240 || item.Position < 1 || item.Position > 5)
            {
                throw new ArgumentException("Invalid Player!");
            }
            else
            {
                repo.Create(item);
            }
        }
        public Player Read(int id)
        {
            var item = repo.Read(id);
            if (item == null)
            {
                throw new ArgumentException("Player doesn't exist");
            }
            else
            { return item; }
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public IEnumerable<Player> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Player item)
        {
            repo.Update(item);
        }

    }
}
