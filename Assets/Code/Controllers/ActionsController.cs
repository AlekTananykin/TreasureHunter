using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    class ActionsController: IExecute, IInitialization, IAttackSystem
    {
        private IDictionary<LootName, IAction> _attackSystems = 
            new Dictionary<LootName, IAction>();

        private ControllersStorage _controllers = new ControllersStorage();

        public void Add(LootName name, IInteractionObject actionSystem)
        {
            if (!(actionSystem is IAction action))
                throw new GameException(
                    "ActionControllersStorage.Add: not an IAttackSystem. ");

            _controllers.Add(actionSystem);
            _attackSystems.Add(name, action);
        }

        public bool Attack(Vector3 place, Vector3 targetPoint, IThing attackThing)
        {
            if (!_attackSystems.TryGetValue(attackThing.Name, out IAction attack))
                return false;

            attack.Attack(place, targetPoint, attackThing);
            return true;
        }

        public void Execute(float deltaTime)
        {
            _controllers.Execute(deltaTime);
        }

        public void Initialize()
        {
            _controllers.Initialize();
        }
    }
}
