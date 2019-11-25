using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CharacterMovement CharacterMovement;


    public void BackToGame()
    {
        CharacterMovement.enabled = !CharacterMovement.enabled;
    }











}
