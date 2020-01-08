using UnityEngine;
using XInputDotNetPure;

public class Explosion : MonoBehaviour
{
    public GameObject me = default;
    public float lifeTime = 0.3f;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private void Start()
    {
        GamePad.SetVibration(playerIndex, 1f, 1f);
    }
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            //je meurt
            GamePad.SetVibration(playerIndex, 0, 0);
            Destroy(me);
        }
    }

    void DetectController()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testetat = GamePad.GetState(testPlayerIndex);
                if (testetat.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
}
