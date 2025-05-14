using TMPro;
using UnityEngine;

namespace BachelorProject.UI
{
    public class InteractionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text interactionText;

        public void SetInteractionText(StringVariable str)
        {
            interactionText.text = str.Value;
        }

        public void ClearInteractionText()
        {
            interactionText.text = string.Empty;
        }
    }
}
