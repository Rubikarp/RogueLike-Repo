using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEntery : MonoBehaviour
{
    public GameObject A_Button, Menu;
    public  CharacterState perso;
    public bool isInMenu = false;

    void Start()
    {
        A_Button.SetActive(false);
        Menu.SetActive(false);
    }

    void Update()
    {
        if(isInMenu == false)
        {
            perso.canMove = true;
        }
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
                perso.canMove = false;
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

