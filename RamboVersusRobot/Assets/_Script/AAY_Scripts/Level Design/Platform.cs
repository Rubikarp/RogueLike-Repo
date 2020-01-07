using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Vector3 velocity;
        private int position = 0;

        public GameObject pointA;
        public GameObject pointB;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.collider.transform.SetParent(transform);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.collider.transform.SetParent(null);
            }
        }
        void Start()
        {
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            while (true)
            {
                switch (position)
                {
                    case 1:
                        transform.position -= (velocity * Time.deltaTime);
                        if (transform.position.x <= pointA.transform.position.x)
                        {
                            position -= 1;
                        }
                        break;

                    default:
                        transform.position += (velocity * Time.deltaTime);
                        if (transform.position.x >= pointB.transform.position.x)
                        {
                            position += 1;
                        }
                        break;
                }
                yield return new WaitForSeconds(0);
            }
        }
    }
}