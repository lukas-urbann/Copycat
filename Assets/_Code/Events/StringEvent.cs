using System;
using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/String Event")]
    public class StringEvent : ParameterEvent
    {
        [SerializeField] private StringReference stringReference;
        
        public void Execute(string value)
        {
            base.Execute(value);
        }
        
        public void Execute(StringReference reference)
        {
            base.Execute(reference.Value);
        }

        public void Execute()
        {
            base.Execute(stringReference.Value);
        }
    }
}