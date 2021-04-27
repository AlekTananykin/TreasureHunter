using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class HeroController: IExecute, IInitialization
    {
        private PersonModel _model;
        private GameObject _view;
        private float _ceenterY;

        internal HeroController(PersonModel model, GameObject view)
        {
            _model = model;
            _view = view;

            if (null == _model || null == _view)
                throw new Exception("PlayerController is not ready. ");
        }

        QeueLeash _leash;

        public Action<Vector3, Vector3> Shoot { get; internal set; }

        public void Execute(float deltaTime)
        {
            if (!_leash.IsNeedMove)
                return;

            _view.transform.position = _leash.Execute(deltaTime, _model.Speed);
        }

        public void Initialize()
        {
            _view.transform.position = _model.InitPosition;
            CapsuleCollider collider = _view.GetComponent<CapsuleCollider>();
            _ceenterY = collider.bounds.size.y / 2;

            _leash = new QeueLeash(_model.InitPosition);
        }

        public void AddNewTargetPoint(Vector3 position)
        {
            Vector3 heroNewPosition = new Vector3(position.x,
                position.y + _ceenterY,
                position.z);

            Debug.Log(_ceenterY);

            _leash.AddPoint(heroNewPosition);
        }

        internal void HitToPoint(Vector3 targetPoint)
        {
            Shoot?.Invoke(
                _view.transform.position + Vector3.up * 2, targetPoint);
        }

        internal Vector3 GetPosition()
        {
            return _view.transform.position;
        }
    }

}
