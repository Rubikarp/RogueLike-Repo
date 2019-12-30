using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAhead : MonoBehaviour
{
    //pour voir l'input sur l'axe X
    [SerializeField] private float direction = 0;

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

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
