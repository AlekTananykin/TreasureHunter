using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class HeroController: PersonController, IInitialization, IManagedPerson
    {
        internal HeroController(IPersonModel model, GameObject view,
             IActionSystem actionSystem)
            :base(model, view, actionSystem)
        {
        }

        public void Initialize()
        {
            CapsuleCollider collider = _view.GetComponent<CapsuleCollider>();
            float ceenterY = collider.bounds.size.y / 2;

            Initialize(new QeueLeash(Model.InitPosition), ceenterY);
        }

        public void GoToPoint(Vector3 position)
        {
            AddNewTargetPoint(position);
        }

        public new void HitToPoint(Vector3 targetPoint)
        {
            base.HitToPoint(targetPoint);
        }

        public new void SelectAction(int actionNumber)
        {
            base.SelectAction(actionNumber);
        }

        public new void TakeLoot(Vector3 targetPoint)
        {
            base.TakeLoot(targetPoint);
        }
    }
}
