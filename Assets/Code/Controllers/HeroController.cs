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
            _leash = new Leash(_model.InitPosition);
        }

        public void AddNewTargetPoint(Vector3 point)
        {
            _leash.AddPoint(point);
        }
    }
}
