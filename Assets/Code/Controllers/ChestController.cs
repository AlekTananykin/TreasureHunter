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
    internal sealed class ChestController : IInitialization
    {
        ChestModel _model;
        GameObject _view;

        public ChestController(ChestModel model, GameObject view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _view.transform.position = _model.Position;
        }
    }
}
