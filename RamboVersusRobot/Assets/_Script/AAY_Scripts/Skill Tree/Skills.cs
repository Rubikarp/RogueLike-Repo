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

                if (SD.skillIcon)
                    SD.skillIcon.sprite = Icon;

                if (SD.skillMoneyNeeded)
                    SD.skillMoneyNeeded.text = MoneyNeeded.ToString() + "Money";

                if (SD.skillAttribute)
                    SD.skillAttribute.text = AffectedAttributes[0].attribute.ToString();

                if (SD.skillAttrAmount)
                    SD.skillAttrAmount.text = "+" + AffectedAttributes[0].amount.ToString();
            }
        }

        public bool CheckSkills (PlayerStats Player)
        {
            if (Player.PlayerMoney < MoneyNeeded)
                return false;

            return true;
        }

        public bool EnableSkill (PlayerStats Player)
        {
            List<Skills>.Enumerator skills = Player.PlayerSkills.GetEnumerator();
            while (skills.MoveNext())
            {
                var CurrSkill = skills.Current;
                if (CurrSkill.name == this.name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetSkill (PlayerStats Player)
        {
            int i = 0;
            List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
            while (attributes.MoveNext())
            {
                List<PlayerAttributes>.Enumerator PlayerAttr = Player.Attributes.GetEnumerator();
                while (PlayerAttr.MoveNext())
                {               
                    if (attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString())
                    {
                        PlayerAttr.Current.amount += attributes.Current.amount;
                        i++;
                    }
                }
            }
            if (i > 0)
            {
                Player.PlayerMoney -= this.MoneyNeeded;
                Player.PlayerSkills.Add(this);
                return true;
            }
            return false;
        }
        
    }

}
