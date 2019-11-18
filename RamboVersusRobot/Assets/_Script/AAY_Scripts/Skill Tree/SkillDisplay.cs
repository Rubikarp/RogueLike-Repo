using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace SkillTree
{


    public class SkillDisplay : MonoBehaviour
    {
        public Skills skill;
        public Text skillName;
        public Text skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillMoneyNeeded;
        public Text skillAttribute;
        public Text skillAttrAmount;

        [SerializeField]
        private PlayerStats m_PlayerHandler;

        // Start is called before the first frame update
        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            m_PlayerHandler.onMoneyChange += ReactToChange;

            if (skill)
                skill.SetValues(this.gameObject, m_PlayerHandler);

            EnableSkills();
        }

        public void EnableSkills()
        {
            if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }

            else if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
            {
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);

            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        public void OnEnable()
        {
            EnableSkills();           
        }

        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        public void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }

        public void TurnOffSkillIcon()
        {
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
        }

        void ReactToChange()
        {
            EnableSkills();
        }
    }
}
