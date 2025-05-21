using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Management
{
    public static class ObjectRegistry
    {
        private static Dictionary<ObjectIdentifier, GameObject> _registry = new();

        public static void Register(ObjectIdentifier id, GameObject obj)
        {
            _registry.TryAdd(id, obj);
        }

        public static void Unregister(ObjectIdentifier id)
        {
            _registry.Remove(id);
        }

        public static GameObject GetObject(ObjectIdentifier id)
        {
            return !id ? null : _registry.GetValueOrDefault(id);
        }

        public static bool GetObject(ObjectIdentifier id, out GameObject go)
        {
            if (!id)
            {
                go = null;
                return false;
            }
            
            if (_registry.TryGetValue(id, out var obj))
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