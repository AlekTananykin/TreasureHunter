using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Person
{
    internal sealed class PersonActionSystem
    {
        internal GameObject View { get; set; }
        internal IPersonModel Model { get; set; }

        private IAttackSystem[] _actions;
        private int _selectedAction = 0;

        private IActionStorage _actionStorage;

        public PersonActionSystem(IActionStorage actionStorage)
        {
            _actionStorage = actionStorage;
        }

        internal void ReloadFromModel()
        {
            int i = 0;
            _actions = new IAttackSystem[Model.AppliedItems.Count];
            foreach (var thing in Model.AppliedItems)
                 _actions[i ++] = _actionStorage.GetAction(thing.Key);
        }

        public void ActionToPoint(Vector3 targetPoint)
        {
            if (null == _actions || 0 == _actions.Length)
                return;

            _actions[_selectedAction].Attack(
                View.transform.position + Vector3.up * 2, targetPoint);
        }

        public void SelectAction(int actionNumber)
        {
            if (_selectedAction >= _actions.Length)
                throw new GameException("PersonActionSystem.ActionToPoint: " +
                    "selected attack number out of range > " + _selectedAction);

            _selectedAction = actionNumber;
        }
    }
}
