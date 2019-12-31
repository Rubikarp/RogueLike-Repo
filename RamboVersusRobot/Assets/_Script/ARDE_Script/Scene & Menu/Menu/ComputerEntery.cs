using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEntery : MonoBehaviour
{
    public GameObject A_Button, Menu;
    public ARDE_2DCharacterMovement CharacterMovement;
    public bool isInMenu = false;

    void Start()
    {
        A_Button.SetActive(false);
        //Menu.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        A_Button.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        A_Button.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("attackLight"))
        {
            if (!isInMenu)
            {
                CharacterMovement.enabled = !CharacterMovement.enabled;
                Menu.SetActive(true);
                isInMenu = true;
            }
        }
    }


    public void outOfMenu()
    {
        isInMenu = false;
    }

}

