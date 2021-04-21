using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
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
        GameObjectsPool<PathMarksFabric> _pathMarkPool;
        const uint _marksPoolSize = 5;

        internal PlayerController(IPlayerInput input, GameObject camera)
        {
            _input = input;
            _camera = camera.GetComponent<Camera>();
            _pathMarkPool = new GameObjectsPool<PathMarksFabric>(
                new PathMarksFabric(), _marksPoolSize);
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
 
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            GameObject go = _pathMarkPool.Create();
            go.transform.position = hit.point;
            PathMarkScript script = go.GetComponent<PathMarkScript>();
            if (null == script)
                throw new GameException("PathMarkScript is not attached. ");

            script.OnPathMatkCall = OnPathMarkTrigger;

            Select_Point(hit.point);
        }

        public SelectPoint Select_Point;
        public void OnPathMarkTrigger(GameObject pathObj)
        {
            PathMarkScript script = pathObj.GetComponent<PathMarkScript>();
            script.OnPathMatkCall -= OnPathMarkTrigger;
            _pathMarkPool.Intake(ref pathObj);
        }
    }
}
