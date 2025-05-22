using System;
using UnityEngine;

namespace BachelorProject.Management
{
    /// <summary>
    /// Identifikator objektu, ktery se zaregistruje do databaze objektu.
    /// Velmi usnadnuje praci s predmety. Velmi uzitecne pro ziskavani referenci na predmety. (Za pomoci ObjectRegistry)
    /// </summary>
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