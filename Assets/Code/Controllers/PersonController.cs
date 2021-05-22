using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal abstract class PersonController : 
        IExecute, IPersonController, IModelController
    {
        protected PersonModel _model;

        protected GameObject _view;

        private float _ceenterY;

        private IPersonMover _personMover;
        private PersonLootSystem _lootSystem;
        private PersonActionSystem _actionSystem;

        public PersonController(PersonModel model, GameObject view, 
            IActionSystem actionSystem)
        {
            _model = model;
            _view = view;

            _lootSystem = new PersonLootSystem() {View =  _view, Model = _model };
            _actionSystem = new PersonActionSystem(actionSystem) 
                { View = _view, Model = _model };
        }

        internal void Initialize(ILeash leash, float ceenterY)
        {
            if (null == _model || null == _view)
                throw new Exception("PersonController is not ready. ");

            _view.transform.position = _model.InitPosition;

            _personMover = new PersonMover(leash, _model.RotationSpeed);
            _ceenterY = ceenterY;
        }

        public virtual void Execute(float deltaTime)
        {
            if (!_personMover.IsNeedMove)
                return;

            Vector3 direction;
            (_view.transform.position, direction) =
                _personMover.Execute(deltaTime, _model.Speed);

            if (direction.sqrMagnitude > 0)
                _view.transform.forward = direction;
        }

        protected void AddNewTargetPoint(Vector3 position)
        {
            Vector3 heroNewPosition = new Vector3(position.x,
                position.y + _ceenterY, position.z);

            _personMover.AddPoint(heroNewPosition);
        }

        protected void HitToPoint(Vector3 targetPoint)
        {
            _actionSystem.ActionToPoint(targetPoint);
        }

        protected void SelectAction(int actionNumber)
        {
            _actionSystem.SelectAction(actionNumber);
        }

        protected void TakeLoot(Vector3 targetPoint)
        {
            _lootSystem.TakeLoot(targetPoint);
        }

        public Vector3 Position => _view.transform.position;

        public int Id => _model.ModelId;

        public void SetModel(IModel model)
        {
            if (model is PersonModel)
            {
                _model = model as PersonModel;
                _view.transform.position = _model.InitPosition;
            }
            else throw new GameException(
                "PersonController.SetModel: model as not PersonModel. ");
        }

        public void PreSafe()
        {
            _model.InitPosition = _view.transform.position;
        }
    }
}
