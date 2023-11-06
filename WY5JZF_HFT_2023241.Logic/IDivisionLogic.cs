using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_HFT_2023241.Logic
{
    public interface IDivisionLogic
    {
        void Create(Division item);
        void Delete(int id);
        IEnumerable<Division> ReadAll();
        void Update(Division item);
        Division Read(int id);

    }
}
