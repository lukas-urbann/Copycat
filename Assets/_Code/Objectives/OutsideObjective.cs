using UnityEngine;


namespace BachelorProject.Objectives
{
    public class OutsideObjective : LevelObjective
    {
        [SerializeField] private bool casperRequirement = false;
        [SerializeField] private bool melchiorRequirement = false;
        [SerializeField] private bool baltazarRequirement = false;

        public void CheckRequirements()
        {
            if (casperRequirement && melchiorRequirement && baltazarRequirement)
            {
                OnObjectiveSuccess();
            }
            else
            {
                OnObjectiveUnsuccessful();
            }
        }

        public void SetCasperRequirement(bool value)
        {
            casperRequirement = value;
            CheckRequirements();
        }
        public void SetMelchiorRequirement(bool value)
        {
            melchiorRequirement = value;
            CheckRequirements();
        }
        public void SetBaltazarRequirement(bool value)
        {
            baltazarRequirement = value;
            CheckRequirements();
        }
    }
}
