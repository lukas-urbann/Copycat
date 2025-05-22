using BachelorProject.Events;
using UnityEngine;

namespace BachelorProject.Objectives
{
    /// <summary>
    /// Zaklad pro skripty sledujici objekty v levelech
    /// </summary>
    public abstract class LevelObjective : MonoBehaviour
    {
        public VoidEvent ObjectiveSuccess;
        public VoidEvent ObjectiveUnuccess;

        public abstract void CheckRequirements();
        
        public void OnObjectiveUnsuccessful()
        {
            ObjectiveUnuccess?.Execute();
        }

        public void OnObjectiveSuccess()
        {
            ObjectiveSuccess?.Execute();
        }
    }
}