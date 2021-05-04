using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate IList<IThing> GetStorage();

public class BagScript : MonoBehaviour, IStorage
{
    public IList<IThing> GetItems()
    {
        if (null == Get_Storage)
            return null;

        return Get_Storage();
    }

    public GetStorage Get_Storage;
}
