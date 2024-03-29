﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisBehavior : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    protected Rigidbody2D myBody = null;
    protected CircleCollider2D myCollider = null;
    protected Transform player = null;
    protected ARDE_SoundManager soundManager = default;

    [Header("à def")]
    public LayerMask TerrainLayerMask;

    [Header("tweaking")]
    public float speed = 10;

    [Space(10)]
    public float detectionRange = 40;
    public float ToFarDistance = 20;
    public float ToNearDistance = 10;

    [Header("inside")]
    //Private Values
    [SerializeField] public Vector3 playerDirection;
    [SerializeField] public float playerDistance;

    [Space(10)]
    public bool playerDetecting;
    public bool playerToNear;
    public bool playerToFar;

    [Space(10)]
    public bool IsAttacking;


    private void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<ARDE_SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region Variables

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        #endregion

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);
    }

    protected void PlayerRelativeToTheEnnemy(float playerDistance, float detectionRange, float ToNearDistance, float ToFarDistance)
    {
        //Est ce que je detecte le joueur ?
        if (detectionRange > playerDistance)
        {
            playerDetecting = true;

            //je m'approche le joueur s'il est trop proche
            if (playerDistance < ToNearDistance)
            {
                playerToNear = true;
            }
            //je m'eloigne du joueur s'il est trop loin
            else if (playerDistance > ToFarDistance)
            {
                playerToNear = false;
                playerToFar = true;
            }
            else
            {
                playerToNear = false;
                playerToFar = false;
            }
        }
        else
        {
            playerDetecting = false;
            playerToNear = false;
            playerToFar = false;
        }
    }

}
