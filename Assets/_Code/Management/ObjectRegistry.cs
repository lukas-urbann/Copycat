using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Management
{
    public static class ObjectRegistry
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
            if (id == null)
                return null;

            if (registry.TryGetValue(id, out var obj))
            {
                return obj;
            }
            else
            {
                return null;
            }
        }
    
        public static bool GetObject(ObjectIdentifier id, out GameObject go)
        {
            if (registry.TryGetValue(id, out var obj))
            {
                go = obj;
                return true;
            }
            else
            {
                go = null;
                return false;
            }
        }
    }
}