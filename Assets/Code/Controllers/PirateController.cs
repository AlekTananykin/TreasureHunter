using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Views;
using System;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class PirateController : PersonController, IInitialization
    {
        private IPersonController _hero;
        
        const float _shootInterval = 3f;
        float _shootTime = _shootInterval;
        const float _seeDistance = 10f;

        internal PirateController(IPersonModel model, GameObject view,
            IActionSystem actionSystem,
            IPersonController hero)
            :base(model, view, actionSystem)
        {
            _hero = hero;
        }

        public override void Execute(float deltaTime)
        {
            base.Execute(deltaTime);

            if ((this.Position - _hero.Position).magnitude < _seeDistance)
            {
                _view.transform.LookAt(_hero.Position);
                _shootTime += deltaTime;
                if (_shootTime >= _shootInterval)
                {
                    _shootTime = 0;
                    HitToPoint(_hero.Position);
                }
                return;
            }
            _shootTime = _shootInterval;
        }

        public void Initialize()
        {
            base.Initialize(new LoopLeash(Model.InitPosition), 0);
        }

        public new void SelectAction(int action)
        {
            base.SelectAction(action);
        }
    }
}
