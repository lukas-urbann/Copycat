using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/GameObject Event")]
    public class GameObjectEvent : ParameterEvent<GameObject>
    {
        [SerializeField] private ObjectIdentifier objectIdentifier;
        
        public void Execute(GameObject value)
        {
            base.Execute(value);
        }
        
        public void Execute(ObjectIdentifier identifier)
        {
            base.Execute(identifier.GetSourceObject());
        }

        public void Execute()
        {
            base.Execute(objectIdentifier.GetSourceObject());
        }
    }
}