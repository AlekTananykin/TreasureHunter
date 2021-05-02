using Assets.Code.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Interfaces
{
    interface IStorage
    {
        IDictionary<LootName, int> GetItems();
    }
}
