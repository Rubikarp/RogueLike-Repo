using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ARDE_EnergiePoint : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    protected Rigidbody2D myBody = null;
    protected CircleCollider2D myCollider = null;
    protected Transform player = null;

    void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
