using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.SaveLoad;
using Assets.Code.Things;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    sealed  internal class ChestModel
    {
        [JsonConverter(typeof(JsonTypeConverter<List<Thing>>))]
        public IList<IThing> Items { get; set; }
        
        public Vector3 Position { get; set; }
    }
}
