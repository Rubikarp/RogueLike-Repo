using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Lever : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private bool doorClosed = true;
        private bool doorWillClose = false;
        public GameObject door;
        public float timer;

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && doorClosed)
            {
                StartCoroutine(OpenDoor());
            }
        }
        private void OnTriggerExit2D (Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !doorWillClose)
            {
                StartCoroutine(CloseDoor());
            }
        }
        IEnumerator OpenDoor()
        {
            doorClosed = false;
            door.transform.Translate(Vector2.up * speed);
            yield return new WaitForSeconds(1);          
            StopCoroutine(OpenDoor());

        }
        IEnumerator CloseDoor()
        {
            doorWillClose = true;
            yield return new WaitForSeconds(timer);          
            door.transform.Translate(Vector2.down * speed);
            yield return new WaitForSeconds(1);
            doorClosed = true;
            doorWillClose = false;
            StopCoroutine(CloseDoor());
        }
    }
}