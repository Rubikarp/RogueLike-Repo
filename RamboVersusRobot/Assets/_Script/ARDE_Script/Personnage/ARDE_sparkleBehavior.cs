using UnityEngine;

public class ARDE_sparkleBehavior : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    protected Transform player = null;
    protected ARDE_SoundManager soundManager = default;

    [Header("tweaking")]
    public float lifeTime = 2f;
    float time = 0f;

    [SerializeField] public Vector2 playerDirection;

    void Start()
    {
        mySelf = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - mySelf.position).normalized;

        FacePlayer();
    }

    void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
