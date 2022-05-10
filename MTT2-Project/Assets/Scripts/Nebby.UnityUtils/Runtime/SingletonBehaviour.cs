using UnityEditor;
using UnityEngine;

namespace Nebby.UnityUtils
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T>
    {
        public static T Instance { get; protected set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                throw new System.InvalidOperationException($"An instance of the singleton {typeof(T).Name} already exists.");
            }
            else
            {
                Instance = (T)this;
            }
        }
    }
}