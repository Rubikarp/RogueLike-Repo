using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Trap : MonoBehaviour
    {

        ARDE_CharacterLifeSystem knockbackScript;
        private int health;
        public Rigidbody2D playerRigidbody;

        // Start is called before the first frame update
        void Start()
        {
            ARDE_CharacterLifeSystem knockbackScript = FindObjectOfType<ARDE_CharacterLifeSystem>();
            GameObject Player = GameObject.Find("Character");
            playerRigidbody = Player.GetComponent<Rigidbody2D>();
            ARDE_CharacterLifeSystem lifeSystem = Player.GetComponent<ARDE_CharacterLifeSystem>();
            health = lifeSystem.health;
        }

        // Update is called once per frame
        void Update()
        {
            //knockbackScript.TakeKnockBack(5, this.transform);
        }
    }
}