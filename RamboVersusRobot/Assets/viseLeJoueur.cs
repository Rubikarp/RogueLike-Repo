using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viseLeJoueur : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    public Transform player = null;

    [SerializeField] public Vector2 playerDirection;

    void Start()
    {
        mySelf = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - mySelf.position).normalized;

        FacePlayer();
    }

    void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
