using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Actions
{
    internal sealed class ActionsStorage: IActionStorage
    {
        private IDictionary<LootName, IAttackSystem> _nameStorage = 
            new Dictionary<LootName, IAttackSystem>();
        private IList<IAttackSystem> _indexStorage = new List<IAttackSystem>();

        public void AddAction(LootName lootName, IAttackSystem attackSystem)
        {
            if (_nameStorage.Keys.Contains(lootName))
                return;

            _nameStorage.Add(lootName, attackSystem);
            _indexStorage.Add(attackSystem);
        }

        public IAttackSystem GetAction(LootName lootName)
        {
            if (0 == _nameStorage.Count)
                return null;

            if (!_nameStorage.TryGetValue(lootName, out IAttackSystem attack))
                throw new GameException(
                    "ActionsStorage.GetAction: unknown lootName: "
                    + lootName);

            return attack;
        }

        public IAttackSystem GetAction(int actionNum)
        {
            if (0 == _nameStorage.Count)
                return null;

            if (actionNum > _indexStorage.Count)
                throw new GameException(
                    "ActionsStorage.GetAction out of range exception." +
                    "actionNum = " + actionNum +
                    "; _indexStorage.Count = " + _indexStorage.Count);

            return _indexStorage[actionNum];
        }

        public int GetActionsCount()
        {
            return _indexStorage.Count;
        }
    }
}
