using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Auxiliary
{
    public enum LootName
    {
        none,

        cutlass,

        grenade,

        gun,

        scope
    }

    public enum LootProperties
    {
        Damage,


        Accuration,
        MovingSpeed,
        Health,
        Force,
        Armor
    }

    public enum ActionCode
    {
        zero, one, two, three, four, five, six, seven, eight, nine
    }
}
