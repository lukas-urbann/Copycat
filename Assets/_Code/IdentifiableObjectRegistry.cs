using System.Collections.Generic;
using UnityEngine;

public static class IdentifiableObjectRegistry
{
    private static Dictionary<ObjectIdentifier, GameObject> registry = new();

    public static void Register(ObjectIdentifier id, GameObject obj)
    {
        if (!registry.ContainsKey(id))
            registry.Add(id, obj);
    }

    public static void Unregister(ObjectIdentifier id)
    {
        registry.Remove(id);
    }

    public static GameObject GetObject(ObjectIdentifier id)
    {
        if (registry.TryGetValue(id, out var obj))
        {
            return obj;
        }
        else
        {
            return null;
        }
    }
}
