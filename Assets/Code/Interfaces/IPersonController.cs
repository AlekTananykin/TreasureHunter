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

        void AddNewTargetPoint(Vector3 position);

        void HitToPoint(Vector3 targetPoint);
        void SelectAction(int actionNumber);
        
        void TakeLoot(Vector3 targetPoint);

        Vector3 Position { get; }
    }
}
