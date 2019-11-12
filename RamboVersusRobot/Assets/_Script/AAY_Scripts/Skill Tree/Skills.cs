using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{
    [CreateAssetMenu(menuName = "Generator/Player/Create Skill")]

    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int MoneyNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

        public void SetValues(GameObject SkillDisplayObject, PlayerStats Player)
        {

            if (SkillDisplayObject)
            {
                SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
                SD.skillName.text = name;
                if (SD.skillDescription)
                    SD.skillDescription.text = Description;
            }
        }
    }
}
