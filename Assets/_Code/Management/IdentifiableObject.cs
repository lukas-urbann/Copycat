using System;
using UnityEngine;

namespace BachelorProject.Management
{
    [Serializable]
    public class IdentifiableObject : MonoBehaviour
    {
        public ObjectIdentifier identifier;

        private void OnEnable()
        {
            if (identifier)
            {
                ObjectRegistry.Register(identifier, this.gameObject);
            }
        }

        private void OnDestroy()
        {
            if (identifier)
            {
                ObjectRegistry.Unregister(identifier);
            }
        }
    }
}