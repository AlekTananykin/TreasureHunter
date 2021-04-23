using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class CameraController : IInitialization, IExecute
    {
        public GameObject _view;
        public CameraModel _model;

        private QeueLeash _leash;

        internal CameraController(CameraModel model, GameObject view)
        {
            _view = view;
            _model = model;
        }

        public void Execute(float deltaTime)
        {
            _view.transform.position = _leash.Execute(deltaTime, _model.Speed);
        }

        public void Initialize()
        {
            if (null == _view || null == _model)
                throw new GameException(this.ToString() + ": is not ready. ");

            _view.transform.position = _model.InitPosition;
            _view.transform.forward = _model.Forward;
            _leash = new QeueLeash(_model.InitPosition);
        }

        public void AddNewTargetPosition(Vector3 position)
        {
            Vector3 cameraNewPosition = new Vector3(position.x,
                _model.Height, 
                position.z);

            _leash.AddPoint(cameraNewPosition);
        }
    }
}
