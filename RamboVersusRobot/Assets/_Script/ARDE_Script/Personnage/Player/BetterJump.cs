using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    CharacterInput input ;
    CharacterState state ;

    [Range(1, 9)] public float fallFactor = 5f;
    [Range(1, 2)] public float lowJumpFactor = 1.42f;

    private void Awake()
    {
        input = this.GetComponent<CharacterInput>();
        state = this.GetComponent<CharacterState>();
    }
    
    void Update()
    {
        if (state.isJumping)
        {
            if (state.body.velocity.y < 0)
            {
                //Si le joueur à fini de monter, il redescend plus vite afin d'avoir un meilleur game feel
                state.body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
            }
            else if (state.body.velocity.y > 0 && !input.jump)
            {
                //Si le joueur lache la touche de saut alors qu'il ne tombe pas sa vitesse vertical est réduite très rapidement (permet un petit saut)
                state.body.velocity = new Vector2(state.body.velocity.x, state.body.velocity.y / lowJumpFactor);
            }
        }
    }


    /*
    augmente la force de gravité si le boutton saut et laché et que le perso monte (-violent mais - efficace)
    state.body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpFactor - 1) * Time.deltaTime;
    */
}
