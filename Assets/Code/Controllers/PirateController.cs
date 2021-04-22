using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Views;
using System;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class PirateController : IExecute
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
    }
}
