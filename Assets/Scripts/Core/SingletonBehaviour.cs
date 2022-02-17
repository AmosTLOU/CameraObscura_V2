using UnityEngine;

namespace Core {
    public class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T> {
        public static T Instance { get; protected set; }
        public virtual void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                Log.E($"Singleton type {typeof(T)} already exists");
            } else {
                Log.I($"Singleton type {typeof(T)} is created");
                Instance = (T) this;
            }
        }

        public virtual void OnDestroy() {
            Instance = null;
        }
    }
}