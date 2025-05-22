using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BachelorProject.Objectives
{
    /// <summary>
    /// Sleduje stavy pacek v levelu Offices
    /// </summary>
    public class OfficeObjective : LevelObjective
    {
        public List<BoolReference> leverValues = new();

        public override void CheckRequirements()
        {
            StartCoroutine(DelayedCheck());
        }

        private IEnumerator DelayedCheck()
        {
            yield return new WaitForEndOfFrame();
            if (leverValues.All(v => v.Value))
            {
                OnObjectiveSuccess();
            }
            else
            {
                OnObjectiveUnsuccessful();
            }
        }
    }
}

