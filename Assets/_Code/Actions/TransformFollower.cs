using UnityEngine;

namespace BachelorProject.Actions
{
    /// <summary>
    /// Následuje zvolený follow, který je určený pomocí ObjectIdentifier.
    /// Velmi užitečné pro sledování např. hráče
    /// </summary>
    public class TransformFollower : MonoBehaviour
    {
        public ObjectIdentifier targetIdentifier;
        private GameObject cachedTargetObject = null;

        public void UpdateTarget(ObjectIdentifier targetIdentifier)
        {
            this.targetIdentifier = targetIdentifier;
            cachedTargetObject = null;
        }

        private void LateUpdate()
        {
            if (!cachedTargetObject)
            {
                cachedTargetObject = targetIdentifier.GetSourceObject();

                if (!cachedTargetObject)
                {
                    Debug.LogWarning($"{GetType().Name} nema follow target!");
                    return;
                }
            }

            transform.position = cachedTargetObject.transform.position;
        }
    }
}