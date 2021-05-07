using Assets.Code.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Interfaces
{
    public interface IThing: ICloneable
    {
        LootName Name { get; set; }
        IDictionary<LootProperties, int> Properties { get; }
        IDictionary<LootName, IThing> Components { get; }
    }
}
