using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Views;
using System;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class PirateController : IExecute, IInitialization
    {
        internal IPersonModel Model { get; }
        private GameObject _view;

        private IPersonController _hero;
        
        const float _shootInterval = 3f;
        float _shootTime = _shootInterval;
        const float _seeDistance = 10f;

        private float _ceenterY;

        private ILeash _leash;
        private IPersonMover _personMover;

        public Vector3 Position
        {
            get 
            {
                return _view.transform.position;
            }
            set
            {
                _view.transform.position = value;
            }
        }

        internal PirateController(IPersonModel model, GameObject view, 
            IPersonController hero)
        {
            Model = model;
            _view = view;
            _hero = hero;

            _leash = new LoopLeash(Model.InitPosition);
            _personMover = new PersonMover(_leash, Model.RotationSpeed);
        }

        public Action<Vector3, Vector3> Attack { get; internal set; }

        public void Execute(float deltaTime)
        {
            if ((this.Position - _hero.Position).magnitude < _seeDistance)
            {
                _view.transform.LookAt(_hero.Position);
                _shootTime += deltaTime;
                if (_shootTime >= _shootInterval)
                {
                    _shootTime = 0;
                    Attack?.Invoke(Position + Vector3.up *2, _hero.Position);
                }
                return;
            }
            _shootTime = _shootInterval;
        }

        public void Initialize()
        {
            Position = Model.InitPosition;

            CapsuleCollider collider = _view.GetComponent<CapsuleCollider>();
            _ceenterY = collider.bounds.size.y / 2;
        }
    }
}
