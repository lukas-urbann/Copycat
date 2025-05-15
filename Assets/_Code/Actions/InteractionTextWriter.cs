using BachelorProject.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Actions
{
    public class InteractionTextWriter : MonoBehaviour
    {
        public List<StringReference> textToWrite = new();
        public ObjectIdentifier interactionLabel;

        private void Start()
        {
            if (interactionLabel == null)
            {
                Debug.LogWarning($"{transform.root.name} nemá Interaction Label");
                return;
            }
        }

        public void Write()
        {
            if (ObjectRegistry.GetObject(interactionLabel, out GameObject interactionLabelObject))
            {
                if (interactionLabelObject.TryGetComponent(out InteractionText text))
                {
                    if (textToWrite.Count > 0)
                    {
                        text.SetInteractionText(textToWrite[Random.Range(0, textToWrite.Count)].Value);
                    }
                    else
                    {
                        Debug.LogWarning($"{transform.root.name} - Žádný text k dispozici");
                    }
                }
                else
                {
                    Debug.LogWarning($"{transform.root.name} - Interaction Label nemá InteractionText");
                }
            }
            else
            {
                Debug.LogWarning($"{transform.root.name} - Interaction Label nenalezen");
            }
        }
    }
}