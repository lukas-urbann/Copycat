using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/Bool Event")]
    public class BoolEvent : ParameterEvent<bool>
    {
        [SerializeField] private bool executeWhenFalse = true;
        [SerializeField] private BoolReference boolReference;
        
        public void Execute(bool value)
        {
            if (!executeWhenFalse && !value) return;
            base.Execute(value);
        }
        
        public void Execute(BoolReference reference)
        {
            if (!executeWhenFalse && !reference.Value) return;
            base.Execute(reference.Value);
        }

        public void Execute()
        {
            if (!executeWhenFalse && !boolReference.Value) return;
            base.Execute(boolReference.Value);
        }
    }
}
