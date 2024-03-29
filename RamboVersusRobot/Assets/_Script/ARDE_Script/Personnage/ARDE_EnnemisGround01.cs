﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisGround01 : ARDE_EnnemisBehavior
{
    [Header("Ground Base")]
    public GameObject attack;
    public Transform attackContainer;
    public Animator animator;
    public float attackCoolDown = 0.8f;
    public float attackDuration = 0.3f;
    public bool haveAttack = false;
    bool isRunning = false;
    bool isAttacking = false;

    public float attackDistance = 10f;



    void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<ARDE_SoundManager>();
        attack.SetActive(false);
    }
    
    void Update()
    {
        #region Variables

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        #endregion

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);

        TrackPlayer();

        animator.SetBool("RobotRunning", isRunning);
        animator.SetBool("RobotAttack", IsAttacking);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, playerDirection.normalized * detectionRange, Color.blue);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToFarDistance, Color.red);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToNearDistance, Color.green);
    }

    protected void TrackPlayer()
    {
        if (IsAttacking)
        {
            IsAttacking = false;
        }

        if (playerDetecting)
        {
            if (playerToNear)
            {
                myBody.velocity += new Vector2(-playerDirection.normalized.x, 0) * speed/20;
            }
            else
            if (playerToFar)
            {
                myBody.velocity += new Vector2(+playerDirection.normalized.x, 0) * speed/30;
            }
            else
            {
                myBody.velocity /= new Vector2(1.1f, 1);
            }
        }

        if (Mathf.Abs(myBody.velocity.x) > 2)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if ((Mathf.Abs(player.position.x - transform.position.x)) < attackDistance && playerDistance < attackDistance * 1.42f)
        {
            if (!haveAttack)
            {
                IsAttacking = true;
                StartCoroutine(Attack(attackCoolDown, attackDuration));
            }
        }
    }

    IEnumerator Attack(float CoolDown, float attackDuration)
    {
        attack.SetActive(true);
        haveAttack = true;
        soundManager.Play("RobotAttack");
        myBody.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(attackDuration);
        attack.SetActive(false);
        myBody.constraints = RigidbodyConstraints2D.None;
        myBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(CoolDown - attackDuration);


        haveAttack = false;

    }
}
