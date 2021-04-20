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
        private HeroModel _model;
        private GameObject _view;
        private float _ceenterY;

        internal HeroController(HeroModel model, GameObject view)
        {
            _model = model;
            _view = view;

            if (null == _model || null == _view)
                throw new Exception("PlayerController is not ready. ");
        }

        Leash _leash;
        
        public void Execute(float deltaTime)
        {
            _view.transform.position = _leash.Execute(deltaTime, _model.Speed);
        }

        public void Initialize()
        {
            _view.transform.position = _model.InitPosition;
            CapsuleCollider collider = _view.GetComponent<CapsuleCollider>();
            _ceenterY = collider.bounds.size.y / 2;

            _leash = new Leash(_model.InitPosition);
        }

        public void AddNewTargetPoint(Vector3 position)
        {
            Vector3 heroNewPosition = new Vector3(position.x,
                position.y + _ceenterY,
                position.z);

            Debug.Log(_ceenterY);

            _leash.AddPoint(heroNewPosition);
        }
    }
}
