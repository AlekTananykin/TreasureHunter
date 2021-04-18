using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
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
        public GameObject View { get; set; }
        public ICameraModel Model {get;set;}


        private Leash _leash;


        public void Execute(float deltaTime)
        {
            View.transform.position = _leash.Execute(deltaTime, Model.Speed);
        }

        public void Initialize()
        {
            if (null == View || null == Model)
                throw new GameException(this.ToString() + ": is not ready. ");

            View.transform.position = Model.InitPosition;
            _leash = new Leash(Model.InitPosition);
        }

        public void AddNewTargetPosition(Vector3 position)
        {
            _leash.AddPoint(position);
        }
    }
}
