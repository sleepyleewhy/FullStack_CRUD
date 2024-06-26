﻿using System;
using System.Collections.Generic;
using System.Linq;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Logic
{
    public class DivisionLogic : IDivisionLogic
    {
        IRepository<Division> repo;
        public DivisionLogic(IRepository<Division> repo)
        {
            this.repo = repo;
        }
        public void Create(Division item)
        {
            if (item.DivisionName.Length > 120 || item.DivisionName == "")
            {
                throw new ArgumentException("Division name does not meet the requirements.");
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

       
        public int AllFansPerDivision(int divisionID)
        {
            return repo.Read(divisionID).Teams.Sum(t => t.FanCount);
        }
        public Team TeamWithMostSalaryCostInDiv(int divID)
        {
            int maxSalary = repo.Read(divID).Teams.Max(t => t.Players.Sum(t => t.Salary));

            return repo.Read(divID).Teams.FirstOrDefault(t => t.Players.Sum(t => t.Salary) == maxSalary);
        }

        public IEnumerable<Player> Top3PointsInDiv(int divisionID)
        {
            return repo.Read(divisionID).Teams.SelectMany(t => t.Players).OrderByDescending(t => t.AvgPoints).Take(3);
        }





    }
}
