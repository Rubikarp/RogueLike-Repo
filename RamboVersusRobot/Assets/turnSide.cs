using UnityEngine;

public class turnSide : MonoBehaviour
{
    protected Transform player = null;
    protected CharacterState state = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = player.GetComponent<CharacterState>();
    }

    void Update()
    {
        //regarder devant soi
        if (state.isLookingRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        //se retourner
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
