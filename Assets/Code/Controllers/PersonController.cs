using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal abstract class PersonController : IPersonController, IExecute
    {
        public IPersonModel Model { get; }

        protected GameObject _view;

        private float _ceenterY;

        private IPersonMover _personMover;

        private PersonLootSystem _lootSystem;

        public PersonController(IPersonModel model, GameObject view)
        {
            Model = model;
            _view = view;

            _lootSystem = new PersonLootSystem() {View =  _view, Model = Model };
        }

        public Action<Vector3, Vector3> Attack { get; internal set; }



        internal void Initialize(ILeash leash, float ceenterY)
        {
            if (null == Model || null == _view)
                throw new Exception("PersonController is not ready. ");

            _view.transform.position = Model.InitPosition;

            _personMover = new PersonMover(leash, Model.RotationSpeed);
            _ceenterY = ceenterY;
        }

        public void Execute(float deltaTime)
        {
            if (!_personMover.IsNeedMove)
                return;

            Vector3 direction;
            (_view.transform.position, direction) =
                _personMover.Execute(deltaTime, Model.Speed);

            if (direction.sqrMagnitude > 0)
                _view.transform.forward = direction;
        }

        public void AddNewTargetPoint(Vector3 position)
        {
            Vector3 heroNewPosition = new Vector3(position.x,
                position.y + _ceenterY, position.z);

            _personMover.AddPoint(heroNewPosition);
        }

        public void HitToPoint(Vector3 targetPoint)
        {
            Attack?.Invoke(
                _view.transform.position + Vector3.up * 2, targetPoint);
        }

        public void SelectAction(int actionNumber)
        {
            throw new NotImplementedException();
        }


        public void TakeLoot(Vector3 targetPoint)
        {
            _lootSystem.TakeLoot(targetPoint);
        }

        public Vector3 Position => _view.transform.position;
    }
}
