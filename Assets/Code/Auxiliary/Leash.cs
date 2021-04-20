using Assets.Code.Interfaces;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Auxiliary
{
    internal class Leash
    {
        private float _treveledDistance;
        private float _targetDistance;

        private Vector3 _lastPoint;
        private Queue<Vector3> _targetPoints = new Queue<Vector3>();

        public Leash(Vector3 currentPoint)
        {
            _lastPoint = new Vector3(currentPoint.x, currentPoint.y, currentPoint.z);
            _targetDistance = 0;
            _treveledDistance = 0;
        }

        public Vector3 Execute(float deltaTime, float speed)
        {
            if (0 == _targetPoints.Count)
                return _lastPoint;

            Vector3 currentTarget = _targetPoints.Peek();

            if (0 == _targetDistance)
                _targetDistance = (_lastPoint - currentTarget).magnitude;

            _treveledDistance = Mathf.Clamp(
                _treveledDistance + speed * deltaTime, 0, _targetDistance);

            const float eps = 0.01f;

            if (_targetDistance <= eps ||
                Math.Abs(_treveledDistance - _targetDistance) <= eps)
            {
                _lastPoint = _targetPoints.Dequeue();
                _targetDistance = 0;
                _treveledDistance = 0;

                return _lastPoint;
            }

            float t = _treveledDistance / _targetDistance;
            return Vector3.Lerp(_lastPoint, currentTarget, t);
        }

        public void AddPoint(Vector3 point)
        {
            _targetPoints.Enqueue(new Vector3(point.x, point.y, point.z));
        }
    }
}
