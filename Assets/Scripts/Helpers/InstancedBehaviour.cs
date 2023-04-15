using System;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// This class is used to automatically declare Instanced behaviours that can be called with {class}.Instance as long as the GameObject with the class exists in the scene
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstancedBehaviour<T> : MonoBehaviour where T: Component
    {
        /// <summary>
        /// Instance of the class that can be called globally as long as the object it's attached to exists in the scene.
        /// </summary>
        public static T Instance;

        protected virtual void Awake()
        {
            // Is the instance not declared?
            if (Instance == null)
            {
                // Declare instance
                Instance = this as T;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}