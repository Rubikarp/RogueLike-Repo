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
        }
                

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
