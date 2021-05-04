using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal class PrickAttackController : IAttackSystem
    {
        readonly float _attackDistance;
        readonly uint _hitCount;
        internal PrickAttackController(float attackDistance, uint hitCount)
        {
            _attackDistance = attackDistance;
            _hitCount = hitCount;
        }

        public void Attack(Vector3 place, Vector3 targetPoint)
        {
            Ray ray = new Ray(place, (targetPoint - place).normalized);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, _attackDistance))
                return;

            if (hitInfo.collider.gameObject.TryGetComponent(
                out IReactToHit react))
                react.Hit(_hitCount);
        }
    }
}
