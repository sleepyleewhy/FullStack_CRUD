using System;
using System.Collections.Generic;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Logic
{
    public class DivisionLogic
    {
        IRepository<Division> repo;
        public DivisionLogic(IRepository<Division> repo)
        {
            this.repo = repo;
        }
        public void Create(Division item)
        {
            if (item.DivisionName.Length > 120)
            {
                throw new ArgumentException("Division name too long!");
            }
            else
            {
                repo.Create(item);
            }
        }
        public Division Read(int id)
        {
            var item = repo.Read(id);
            if (item == null)
            {
                throw new ArgumentException("Division doesn't exist!");
            }
            return item;
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public IEnumerable<Division> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Division item)
        {
            repo.Update(item);
        }
        
    }
}
