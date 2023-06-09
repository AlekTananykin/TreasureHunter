﻿using Assets.Code.Auxiliary;
using Assets.Code.Exceptions;
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
    internal sealed class ThingsRandFabric: IThingsFabric
    {
        private System.Random _rand = new System.Random();

        public IList<IThing> CreateThings(int thingsCount)
        {
            IList<IThing> things = new List<IThing>();
            Array values = Enum.GetValues(typeof(LootName));

            int thingNumber = 0;
            while (thingNumber < thingsCount)
            {
                LootName thingName = (LootName)values.GetValue(
                    _rand.Next(values.Length - 1));

                if (LootName.none == thingName)
                    continue;

                IThing thing = CreateThing(thingName);

                things.Add(thing);

                ++thingNumber;
            }

            return things;
        }

        private IThing CreateRandomThing(LootName thingName)
        {
            IThing thing = new Thing() { Name = thingName };

            Array values = Enum.GetValues(typeof(LootProperties));
            int propertyCount = _rand.Next(3);
            int propertyNumber = 0;

            while (propertyNumber < propertyCount)
            {
                LootProperties propertyName = (LootProperties)values.GetValue(
                    _rand.Next(values.Length - 1));

                if (thing.Properties.ContainsKey(propertyName))
                    continue;

                propertyNumber++;
                thing.Properties.Add(propertyName, _rand.Next(15));
            }

            return thing;
        }

        private IThing CreateThing(LootName thingName)
        {
            switch (thingName)
            {
                case LootName.cutlass:
                    return CreateCutlass();
                case LootName.grenade:
                    return CreateGrenade();
                case LootName.gun:
                    return CreateGun();
                case LootName.scope:
                    return CreateScope();
                default:
                    throw new GameException(
                        "ThingsRandFabric: Unknown loot type. ");
            }
        }

        private IThing CreateCutlass()
        {
            IThing thing = new Thing() { 
                Name = LootName.cutlass, 
                Cost = _rand.Next(12), 
                Target = LootName.none };
            thing.Properties.Add(LootProperties.Damage, 4);
            
            return thing;
        }

        private IThing CreateGrenade()
        {
            IThing thing = new Thing()
            {
                Name = LootName.grenade,
                Cost = _rand.Next(1, 5),
                Target = LootName.none
            };
            thing.Properties.Add(LootProperties.Damage, 4);

            return thing;
        }

        private IThing CreateGun()
        {
            IThing thing = new Thing()
            {
                Name = LootName.gun,
                Cost = _rand.Next(12),
                Target = LootName.none
            };
            thing.Properties.Add(LootProperties.Damage, 4);
            return thing;
        }

        private IThing CreateScope()
        {
            IThing thing = new Thing()
            {
                Name = LootName.scope,
                Cost = _rand.Next(120),
                Target = LootName.gun
            };
            thing.Properties.Add(LootProperties.Accuration, 4);
            return thing;
        }

    }
}
