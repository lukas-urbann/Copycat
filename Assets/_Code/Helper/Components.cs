using UnityEngine;

namespace BachelorProject.Helper
{
    public static class Components
    {
        /// <summary>
        /// Najde komponent v rootu objektu a rekurzivně prohledá jeho děti
        /// </summary>
        public static T FindComponentFromRoot<T>(this Transform input) where T : Component
        {
            if (HasComponent<T>(input.transform.root))
                return input.transform.root.GetComponent<T>();

            return GetComponentInChildrenRecursively<T>(input.transform.root);
        }

        /// <summary>
        /// Pouze zjišťuje, zda komponent existuje na daném objektu
        /// </summary>
        public static bool HasComponent<T>(this Transform input) where T : Component
        {
            if (input.GetComponent<T>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Získá komponent z dětí objektu rekurzivně
        /// </summary>
        public static T GetComponentInChildrenRecursively<T>(this Transform _transform) where T : Component
        {
            if (HasComponent<T>(_transform))
                return _transform.GetComponent<T>();

            foreach (Transform obj in _transform)
            {
                if (HasComponent<T>(obj)) return obj.GetComponent<T>();

                T component;

                foreach (Transform objChild in obj)
                {
                    component = GetComponentInChildrenRecursively<T>(objChild);
                    if (component != null) return component;
                }
            }

            return null;
        }
    }
}
