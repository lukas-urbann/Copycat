using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Actions
{
    public class BooleanToggle : MonoBehaviour
    {
        public bool value = false;

        public UnityEvent<bool> onValueChanged;

        public void InvokeAction(bool value)
        {
            this.value = value;
            onValueChanged?.Invoke(value);
        }

        public void ToggleBoolean()
        {
            onValueChanged?.Invoke(value = !value);
        }
    }
}
