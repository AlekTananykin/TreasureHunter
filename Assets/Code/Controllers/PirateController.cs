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

        internal PirateController(IPersonModel model, GameObject view)
        {
            _model = model;
            _view = view;
        }

        public void Execute(float deltaTime)
        {
        }

        public void Initialize()
        {
            _view.transform.position = _model.InitPosition;
        }
    }
}
