using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    internal sealed class ChestController : IInitialization, ICleanup, 
        IModelController
    {
        public ChestModel Model { get; }

        public int Id => Model.ModelId;

        private GameObject _view;

        public ChestController(ChestModel model, GameObject view)
        {
            Model = model;
            _view = view;
        }

        public void Initialize()
        {
            BagScript storageScript = _view.AddComponent<BagScript>();
            storageScript.Get_Storage += GetThings;

            _view.transform.position = Model.Position;
        }

        public IList<Thing> GetThings()
        {
            return Model.Items;
        }

        public void Cleanup()
        {
            //BagScript storageScript = _view.GetComponent<BagScript>();
            //if (null != storageScript?.Get_Storage)
            //    storageScript.Get_Storage -= GetThings;
        }
    }
}
