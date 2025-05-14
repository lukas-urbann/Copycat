using System;
using UnityEngine;

[Serializable]
public class IdentifiableObject : MonoBehaviour
{
    public ObjectIdentifier identifier;

    private void OnEnable()
    {
        if (identifier != null)
        {
            IdentifiableObjectRegistry.Register(identifier, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (identifier != null)
        {
            IdentifiableObjectRegistry.Unregister(identifier);
        }
    }
}
