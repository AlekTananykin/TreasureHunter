using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Interfaces
{
    internal interface IThingsFabric
    {
        IList<IThing> CreateThings(int thingsCount);
    }
}
