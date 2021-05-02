using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    interface IPersonController
    {
        IPersonModel Model { get; }

        Action<Vector3, Vector3> Shoot { get;}

        void AddNewTargetPoint(Vector3 position);

        void HitToPoint(Vector3 targetPoint);
        
        void TakeLoot(Vector3 targetPoint);

        Vector3 GetPosition();

    }
}
