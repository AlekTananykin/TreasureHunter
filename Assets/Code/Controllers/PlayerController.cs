using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.PlayerInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    delegate void SelectPoint(Vector3 point);

    internal sealed class PlayerController : IExecute, IInitialization
    {
        private IPlayerInput _input;
        Camera _camera;

        internal PlayerController(IPlayerInput input, GameObject camera)
        {
            _input = input;
            _camera = camera.GetComponent<Camera>();
        }

        public void Initialize()
        {
            if (null == _input || null == _camera)
                throw new GameException(
                    "PlayerController: input is absent. ");
        }

        public void Execute(float deltaTime)
        {
            if (!_input.IsSelected)
                return;

            Ray ray = _camera.ScreenPointToRay(
                new Vector3(_input.MoveX, _input.MoveY, 0));

            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
            
            Select_Point(hit.collider.transform.position);
        }

        public SelectPoint Select_Point;
    }
}
