using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/Float Event")]
    public class FloatEvent : ParameterEvent
    {
        [SerializeField] private FloatReference floatReference;
        
        public void Execute(float value)
        {
            base.Execute(value);
        }
        
        public void Execute(FloatReference reference)
        {
            base.Execute(reference.Value);
        }

        public void Execute()
        {
            base.Execute(floatReference.Value);
        }
    }
}
