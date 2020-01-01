using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAhead : MonoBehaviour
{
    protected Transform player = null;
    //pour voir l'input sur l'axe X
    [SerializeField] private float direction = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 playerDirection = (player.position - transform.position);
        direction = playerDirection.x;

        lookAhead();
    }

    private void lookAhead()
    {
        //regarder devant soi
        if (direction > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        //se retourner
        else if (direction < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
