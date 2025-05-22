using TMPro;
using UnityEngine;

namespace BachelorProject.UI
{
    /// <summary>
    /// Prepisuje hlavni interakcni text v UI
    /// </summary>
    public class InteractionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text interactionText;

        public void SetInteractionText(StringVariable str)
        {
            interactionText.text = str.Value;
        }

        public void SetInteractionText(string str)
        {
            interactionText.text = str;
        }

        public void ClearInteractionText()
        {
            interactionText.text = string.Empty;
        }
    }
}
