using Assets.Code.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Interfaces
{
    interface IActionStorage
    {
        void AddAction(LootName lootName, 
            IAttackSystem attackSystem);
        IAttackSystem GetAction(LootName lootName);
        IAttackSystem GetAction(int actionNum);
        int GetActionsCount();
    }
}
