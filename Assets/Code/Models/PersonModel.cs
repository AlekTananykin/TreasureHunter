using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    internal class PersonModel: IPersonModel
    {
        public Vector3 InitPosition { get; set; }
        public float Speed { get; set; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        
        public int Skill { get; set; }

        public IDictionary<LootName, IList<IThing>> BagItems { get; }
        public IDictionary<LootName, IThing> AppliedItems { get; }

        public float RotationSpeed { get; set; }

        public PersonModel()
        {
            BagItems = new Dictionary<LootName, IList<IThing>>();
            AppliedItems = new Dictionary<LootName, IThing>();
        }
    }
}
