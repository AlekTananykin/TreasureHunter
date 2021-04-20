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
            Vector3 position = new Vector3();
            if (!_input.GetClickPosition(ref position))
                return;

            Ray ray = _camera.ScreenPointToRay(position);

            Debug.Log(position);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = hit.point;

            Select_Point(hit.point);
        }

        public SelectPoint Select_Point;
    }
}
