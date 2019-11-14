using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject A_Button;
    public GameObject Canvas;
    
    void Start()
    {
        A_Button.SetActive(false);
        Canvas.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        A_Button.SetActive(true);
            if (Input.GetButton("Saut"))
            {
                Debug.Log("Test");
                Canvas.SetActive(true);
            }
            if (Input.GetButton("Dash"))
            {
                Canvas.SetActive(false);
            }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        A_Button.SetActive(false);
    }
}
