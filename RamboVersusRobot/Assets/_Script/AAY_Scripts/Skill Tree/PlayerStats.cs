using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree {

public class PlayerStats : MonoBehaviour {

    [Header("Main Player Stats")]
    public string PlayerName;
    public int PlayerMoney = 0;
    public int PlayerHP = 100;

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}
