using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
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
        private const float _takeDistance = 4.0f;
        private float _ceenterY;

        private ILeash _leash;
        private IPersonMover _personMover;

        public PersonController(IPersonModel model, GameObject view)
        {
            Model = model;
            _view = view;
        }

        public Action<Vector3, Vector3> Shoot { get; internal set; }

        internal void Initialize(ILeash leash, float ceenterY)
        {
            if (null == Model || null == _view)
                throw new Exception("PersonController is not ready. ");

            _view.transform.position = Model.InitPosition;

            _leash = leash;
            _personMover = new PersonMover(_leash, Model.RotationSpeed);
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

            _leash.AddPoint(heroNewPosition);
        }

        public abstract void HitToPoint(Vector3 targetPoint);


        public void TakeLoot(Vector3 targetPoint)
        {
            Ray ray = new Ray(_view.transform.position, 
                (targetPoint - _view.transform.position).normalized);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo))
                return;

            if (hitInfo.distance > _takeDistance)
                return;


            if (hitInfo.collider.gameObject.TryGetComponent(out IStorage bag))
            {
                IDictionary<LootName, int> loot = bag.GetItems();
                foreach (var item in loot)
                    AddLoot(item.Key, item.Value);

                loot.Clear();
            }
        }

        private void AddLoot(LootName name, int numberOf)
        {
            if (Model.BagItems.ContainsKey(name))
                Model.BagItems[name] = Model.BagItems[name] + numberOf;
            else
                Model.BagItems.Add(name, numberOf);
        }

        public Vector3 GetPosition()
        {
            return _view.transform.position;
        }
    }
}
