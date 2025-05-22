using UnityEngine;

namespace BachelorProject.Objectives
{
    /// <summary>
    /// Sleduje spravne natoceni soch v levelu Outside
    /// Hodnoty se nastavuji pomoc BoolEventu
    /// </summary>
    public class OutsideObjective : LevelObjective
    {
        [SerializeField] private bool casperRequirement = false;
        [SerializeField] private bool melchiorRequirement = false;
        [SerializeField] private bool baltazarRequirement = false;

        public override void CheckRequirements()
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
