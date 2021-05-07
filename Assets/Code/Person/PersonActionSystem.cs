using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Person
{
    internal sealed class PersonActionSystem
    {
        internal GameObject View { get; set; }
        internal IPersonModel Model { get; set; }


        public void ActionToPoint(Vector3 targetPoint)
        {
        }

        public void SelectAction(ActionCode actionNumber)
        {
            
        }

        
    }
}
