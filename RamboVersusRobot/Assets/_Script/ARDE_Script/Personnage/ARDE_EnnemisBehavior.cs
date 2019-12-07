using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisBehavior : MonoBehaviour
{
    [Header("Auto")]
    Transform mySelf = null;
    Rigidbody2D myBody = null;
    CircleCollider2D myCollider = null;
    public Transform player = null;

    [Header("à def")]
    public LayerMask TerrainLayerMask;

    [Header("tweaking")]
    public float speed;
    public float detectionRange, stoppingDistance, retreatDistance;

    //Private Values
    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private Vector2 playerMoveTo;
    [SerializeField] private float playerDistance;

    [SerializeField] private float flyForce = 1f;
    [SerializeField] private float detectDist = 5f;

    private void Start()
    {
        mySelf = this.GetComponent<Transform>();
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
