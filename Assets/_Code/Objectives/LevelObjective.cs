using BachelorProject.Events;
using UnityEngine;

namespace BachelorProject.Objectives
{
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