using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Pools
{
    public abstract class Pool<T> : MonoBehaviour
    {
        [SerializeField] protected List<T> GameObjects;

        protected List<T> GameObjectsInPool;

        protected const int Capacity = 20;

        public abstract void Init();

        public abstract List<T> TryGetObjects(T gameObjectToGet);
    }
}