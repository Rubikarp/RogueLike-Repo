using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Trap : MonoBehaviour
    {

        [SerializeField]
        private float knockback = 1.0f;

        public Rigidbody2D playerRigidbody;

        // Start is called before the first frame update
        void Start()
        {
            playerRigidbody = GameObject.Find("Character").GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D");
                playerRigidbody.AddForce(transform.up * knockback);
            }           
        }
    }
}