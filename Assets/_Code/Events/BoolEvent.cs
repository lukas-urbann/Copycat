using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/Bool Event")]
    public class BoolEvent : ParameterEvent
    {
        [SerializeField] private BoolReference boolReference;
        
        public void Execute(bool value)
        {
            base.Execute(value);
        }
        
        public void Execute(BoolReference reference)
        {
            base.Execute(reference.Value);
        }

        public void Execute()
        {
            base.Execute(boolReference.Value);
        }
    }
}
