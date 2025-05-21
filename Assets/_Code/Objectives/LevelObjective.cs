using BachelorProject.Events;
using UnityEngine;

namespace BachelorProject.Objectives
{
    public class LevelObjective : MonoBehaviour
    {
        public VoidEvent ObjectiveSuccess;
        public VoidEvent ObjectiveUnuccess;

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