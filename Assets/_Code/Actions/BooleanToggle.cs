using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Actions
{
    /// <summary>
    /// Jednoduchá třída pro přepínání boolean hodnoty. K využití z inspectoru.
    /// </summary>
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
