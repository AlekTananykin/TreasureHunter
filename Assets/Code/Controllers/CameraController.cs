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
    internal sealed class CameraController : IInitialization, 
        IExecute, IModelController
    {
        public GameObject _view;
        public CameraModel Model { get; set; }

        private QeueLeash _leash;

        public void SetModel(IModel model)
        {
            if (model is CameraModel)
                Model = model as CameraModel;
            else throw new GameException(
                "CameraController.SetModel: model as not CameraModel. ");
        }
        public int Id => Model.ModelId;

        internal CameraController(CameraModel model, GameObject view)
        {
            _view = view;
            Model = model;
        }

        public void Execute(float deltaTime)
        {
            if (_leash.IsNeedMove)
                _view.transform.position = _leash.Execute(deltaTime, Model.Speed);
        }

        public void Initialize()
        {
            if (null == _view || null == Model)
                throw new GameException(this.ToString() + ": is not ready. ");

            _view.transform.position = Model.InitPosition;
            _view.transform.forward = Model.Forward;
            _leash = new QeueLeash(Model.InitPosition);
        }

        public void AddNewTargetPosition(Vector3 position)
        {
            Vector3 cameraNewPosition = new Vector3(position.x,
                Model.Height, 
                position.z);

            _leash.AddPoint(cameraNewPosition);
        }
    }
}
