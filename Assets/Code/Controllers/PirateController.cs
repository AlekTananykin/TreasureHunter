using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Views;
using System;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class PirateController : IExecute, IInitialization
    {
        IPersonModel _model;
        GameObject _view;
        LoopLeash _leash;
        HeroController _hero;
        
        const float _shootInterval = 3f;
        float _shootTime = _shootInterval;
        const float _seeDistance = 10f;


        internal PirateController(IPersonModel model, GameObject view, 
            HeroController hero)
        {
            _model = model;
            _view = view;
            _hero = hero;
        }

        public Action<Vector3, Vector3> Shoot { get; internal set; }

        public void Execute(float deltaTime)
        {
            if ((_view.transform.position - _hero.GetPosition()).magnitude < _seeDistance)
            {
                _shootTime += deltaTime;
                if (_shootTime >= _shootInterval)
                {
                    _shootTime = 0;
                    Shoot(_view.transform.position + Vector3.up *2, _hero.GetPosition());
                }
                return;
            }
            _shootTime = _shootInterval;
        }

        public void Initialize()
        {
            _view.transform.position = _model.InitPosition;
        }


    }
}
