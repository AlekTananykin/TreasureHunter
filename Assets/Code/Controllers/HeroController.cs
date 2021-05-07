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
    internal sealed class HeroController: PersonController, IInitialization
    {
        internal HeroController(IPersonModel model, GameObject view, 
            IAttackSystem attackSystem)
            :base(model, view, attackSystem)
        {
        }

        public void Initialize()
        {
            CapsuleCollider collider = _view.GetComponent<CapsuleCollider>();
            float ceenterY = collider.bounds.size.y / 2;

            Initialize(new QeueLeash(Model.InitPosition), ceenterY);
        }
    }
}
