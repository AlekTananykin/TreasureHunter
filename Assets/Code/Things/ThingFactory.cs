using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Things
{
    internal sealed class ThingFactory : IThingFactory
    {
        private IDictionary<LootName, IThing> _prototypes;

        internal ThingFactory()
        {
            _prototypes = new Dictionary<LootName, IThing>();
        }

        public IThing CreateThing(LootName name)
        {
            if (!_prototypes.TryGetValue(name, out IThing prototype))
            {
                prototype = CreatePrototype(name);
                if (null == prototype)
                    throw new GameException("CreateThing: Unknown loot name. ");

                _prototypes.Add(name, prototype);
            }

            return (IThing)prototype.Clone();
        }

        private IThing CreatePrototype(LootName name)
        {
            IThing prototype = new Thing();
            prototype.Name = name;

            return prototype;
        }
    }
}
