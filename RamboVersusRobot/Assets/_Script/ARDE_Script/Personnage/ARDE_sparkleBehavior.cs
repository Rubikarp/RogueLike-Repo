using UnityEngine;
using XInputDotNetPure;

public class ARDE_sparkleBehavior : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    public GameObject me = null;
    protected Transform player = null;
    protected ARDE_SoundManager soundManager = default;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    [Header("tweaking")]
    public float lifeTime = 2f;
    float pastLifeTime;

    [SerializeField] public Vector2 playerDirection;

    void Start()
    {
        mySelf = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - mySelf.position).normalized;

        GamePad.SetVibration(playerIndex, 0.6f, 0.6f);

        pastLifeTime = lifeTime;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < (pastLifeTime - 0.2f))
        {
            GamePad.SetVibration(playerIndex, 0, 0);

        }
        if (lifeTime < 0)
        {
            Destroy(me);
        }

        FacePlayer();
    }

    void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
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
