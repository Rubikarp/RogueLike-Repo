using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{

public class PlayerStats : MonoBehaviour
    {

        [Header("Main Player Stats")]
        public string PlayerName;
        public int PlayerHP = 100;

        [SerializeField]
        private int m_PlayerMoney = 0;
        public int PlayerMoney
        {
            get { return m_PlayerMoney; }
            set
            {
                m_PlayerMoney = value;

                if (onMoneyChange != null)
                    onMoneyChange();
            }
        }

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> PlayerSkills = new List<Skills>();

       
        public delegate void OnMoneyChange();
        public event OnMoneyChange onMoneyChange;

            public void UpdateMoney(int amount)
            {
                PlayerMoney += amount;
            }
      }
}
