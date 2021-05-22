using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Auxiliary
{
    public enum LootName
    {
        none = 0,
        cutlass = 1,
        grenade = 2,
        gun = 3,
        scope = 4
    }

    public enum LootProperties
    {
        Damage = 0,
        Accuration = 1,
        MovingSpeed = 2,
        Health = 3,
        Force = 4,
        Armor = 5
    }

    public enum ActionCode
    {
        zero, one, two, three, four, five, six, seven, eight, nine
    }
}
