using BachelorProject.Management;
using UnityEngine;

/// <summary>
/// Reference na GameObject, pro vyuziti s ObjectRegistry
/// Odkazem na tento objekt dokaze ziskat GameObject, ktery je ulozen v ObjectRegistry
/// VELMI uzitecne pro praci s hernimy objekty na scene.
/// Lze ziskat cely objekt nebo jeho komponenty.
/// </summary>
[CreateAssetMenu]
public class ObjectIdentifier : ScriptableObject
{
    public GameObject GetSourceObject()
    {
        return ObjectRegistry.GetObject(this);
    }

    public T GetSourceComponent<T>()
    {
        var sourceObject = GetSourceObject();
        return sourceObject && sourceObject.TryGetComponent(out T comp) ? comp : default(T);
    }
    
    public bool GetSourceComponent<T>(out T comp)
    {
        var sourceObject = GetSourceObject();
        
        if (!sourceObject)
        {
            Debug.LogWarning("ObjectIdentifier: Source object je null");
            comp = default(T);
            return false;
        }
        
        comp = sourceObject.TryGetComponent(out T c) ? c : default(T);
        return true;
    }
}
