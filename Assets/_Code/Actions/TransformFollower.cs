using UnityEngine;

namespace BachelorProject.Actions
{
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
                    Debug.LogWarning($"{GetType().Name} nemá follow target!");
                    return;
                }
            }

            transform.position = cachedTargetObject.transform.position;
        }
    }
}