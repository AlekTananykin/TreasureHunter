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
    internal class PersonLootSystem
    {
        public GameObject View { get; set; }
        public IPersonModel Model { get; set; }

        private const float _takeDistance = 4.0f;

        public void TakeLoot(Vector3 targetPoint)
        {
            Ray ray = new Ray(View.transform.position,
                (targetPoint - View.transform.position).normalized);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo))
                return;

            if (hitInfo.distance > _takeDistance)
                return;

            if (hitInfo.collider.gameObject.TryGetComponent(out IStorage bag))
            {
                IList<IThing> bagItems = bag.GetItems();
                foreach (var item in bagItems)
                    AddLoot(item);

                bagItems.Clear();
            }
        }

        private void AddLoot(IThing thing)
        {
            if (Model.BagItems.ContainsKey(thing.Name))
                Model.BagItems[thing.Name].Add(thing);
            else
                Model.BagItems.Add(thing.Name, new List<IThing>() {thing});

            Debug.Log("Loot: " + thing.Name);
        }
    }
}
