using Assets.Code.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Views
{
    class ViewsFabric
    {
        private GameObject _hero;
        internal GameObject CreateHero()
        {
            if (null == _hero)
            {
                GameObject heroPrefab = 
                    (GameObject)Resources.Load("Prefabs/Heroes/Hero");
                if (null == heroPrefab)
                {
                    throw new GameException("CreateHero: Hero prefab is not found. ");
                }
                _hero = (GameObject)GameObject.Instantiate(heroPrefab);
            }
            return _hero;
        }

        private GameObject _camera;
        internal GameObject CreateCamera()
        {
            if (null == _camera)
            {
                GameObject cameraPrefab = 
                    (GameObject)Resources.Load("Prefabs/Player/Main Camera");
                if (null == cameraPrefab)
                {
                    throw new GameException("CreateCamera: Camera prefab is not found. ");
                }
                _camera = GameObject.Instantiate(cameraPrefab);
            }
            return _camera;
        }

        private GameObject _pathMarkPrefab;
        internal GameObject CreatePathMark()
        {
            return CreateObjectFromFile(
                ref _pathMarkPrefab, "Prefabs/Heroes/PathMark");
        }

        GameObject CreateObjectFromFile(ref GameObject prefab, string prefabPath)
        {
            if (null == prefab)
            {
                prefab = (GameObject)Resources.Load(prefabPath);
                if (null == prefab)
                    throw new GameException("CreateObjectFromFile: " +
                        "Prefab can't be loaded from file \"" + prefabPath + "\"");
            }
            return GameObject.Instantiate(prefab);
        }

    }
}
