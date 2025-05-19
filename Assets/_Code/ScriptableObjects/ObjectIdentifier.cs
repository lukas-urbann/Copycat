using BachelorProject.Management;
using UnityEngine;

[CreateAssetMenu]
public class ObjectIdentifier : ScriptableObject
{
    public GameObject GetSourceObject()
    {
        return ObjectRegistry.GetObject(this);
    }

    public T GetSourceComponent<T>()
    {
        if (GetSourceObject().TryGetComponent(out T comp))
        {
            return comp;
        }

        return default(T);
    }
}
