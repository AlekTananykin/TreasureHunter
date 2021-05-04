using Assets.Code.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    interface IPersonModel
    {
        Vector3 InitPosition { get; set; }
        float Speed { get; set; }
        float RotationSpeed { get; set; }

        int Health { get; set; }
        int MaxHealth { get; set; }
        int Skill { get; set; }

        IDictionary<LootName, IList<IThing>> BagItems { get; }
        IDictionary<LootName, IThing> AppliedItems { get; }
    }
}
