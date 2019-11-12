using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpForNextLevel : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float Ydetection = 0f;

    void Update()
    {
        //si le joueur passe en dessus de la valeur
        if (player.position.y < Ydetection )
        {
            //alors on change de scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
