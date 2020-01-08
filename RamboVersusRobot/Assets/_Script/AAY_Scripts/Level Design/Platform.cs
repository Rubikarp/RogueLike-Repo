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
        public bool horizontal = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Joueur")
            {
                collision.collider.transform.SetParent(transform);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Joueur")
            {
                collision.collider.transform.SetParent(null);
            }
        }
        void Start()
        {
            if (horizontal)
            {
                StartCoroutine(MoveHorizontal());
            }
            else
            {
                StartCoroutine(MoveVertical());
            }           
        }

        IEnumerator MoveHorizontal()
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

        IEnumerator MoveVertical()
        {
            while (true)
            {
                switch (position)
                {
                    case 1:
                        transform.position -= (velocity * Time.deltaTime);
                        if (transform.position.y <= pointB.transform.position.y)
                        {
                            position -= 1;
                        }
                        break;

                    default:
                        transform.position += (velocity * Time.deltaTime);
                        if (transform.position.y >= pointA.transform.position.y)
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