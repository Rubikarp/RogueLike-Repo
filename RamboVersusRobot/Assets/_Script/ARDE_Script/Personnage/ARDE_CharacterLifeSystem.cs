using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class ARDE_CharacterLifeSystem : MonoBehaviour
{
    protected Transform mySelf = default;
    protected Rigidbody2D myBody = default;
    protected GameObject Me = default;
    public ARDE_ScreenShake cameraShake = default;
    public bool haveTakeDamage = false;
    public int health = 5;
    public ARDE_SoundManager soundManager = default;
    public GameObject player = default;
    public ARDE_2DCharacterMovement mouv = default;
    public Fade redOverlay = default;

    [SerializeField]
    private CharacterState etat = default;

    // health already define
    private int maxHealth = 7;

    [Space(10)]
    public int energie = 80;
    public int energieParSec = 3;
    private int maxEnergie = 100;

    private float time = 0f;

    [Range(0, 1)]
    public float bulletScreenShake = 0.2f, attackScreenShake = 0.4f;

    [Range(0.5f, 3)]
    public float knockbackSensitivity = 1f;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevetat;

    private void Start()
    {
        mySelf = this.transform;
        myBody = GetComponentInParent<Rigidbody2D>();

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraShake = cam.GetComponent<ARDE_ScreenShake>();
    }

    private void Update()
    {
        health = ClampInt(health, maxHealth);
        energie = ClampInt(energie, maxEnergie);

        DetectController();
        isAlive(player, 1f);

        time += Time.deltaTime;
        EnergieFill(energieParSec);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!haveTakeDamage)
        {
            if (hit.CompareTag("damage"))
            {
                GameObject objectAttack = hit.gameObject;
                ARDE_AttackSystem attack = objectAttack.GetComponentInParent<ARDE_AttackSystem>();

                //S'il s'agit d'un projectile
                if (attack == null)
                {
                    ARDE_Projectile bullet = objectAttack.GetComponentInParent<ARDE_Projectile>();
                    TakeDamage(bullet.damage);
                    TakeKnockBack(bullet.knockback, bullet.mySelf);

                    cameraShake.trauma += bulletScreenShake;

                    StartCoroutine(damageInvulnerabilty(0.2f));

                    return;
                }

                TakeDamage(attack.damage);
                TakeKnockBack(attack.knockback, attack.attackPos);

                cameraShake.trauma += attackScreenShake;

                StartCoroutine(damageInvulnerabilty(0.3f));
            }
        }

        if (hit.CompareTag("NRJ"))
        {
            gainNRJ(2);
        }

        if (hit.CompareTag("Life"))
        {
            gainLife(1);
        }
    }

    public int ClampInt(int value, int MaxValue)
    {
        return Mathf.Clamp(value, 0, MaxValue);
    }

    private void isAlive(GameObject Me, float DeathScrennShake)
    {
        if (health <= 0)
        {
            cameraShake.trauma += DeathScrennShake;


            soundManager.Play("Mort");

            new WaitForSeconds(0.3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void EnergieFill(int EnergieParSec)
    {
        if (time > (1f / EnergieParSec))
        {
            energie++;
            time = 0f;
        }
    }

    public void gainLife(int bonusHealth)
    {
        health += bonusHealth;
        etat.soundManager.Play("Loot");
    }

    public void gainNRJ(int bonusNRJ)
    {
        energie += bonusNRJ;
        etat.soundManager.Play("Loot");
    }

    public void energieAttack(int energieCost)
    {
        energie -= energieCost;
    }

    private void TakeKnockBack(float knockbackPower, Transform attackSource)
    {
        // La direction de l'attaque
        Vector2 knockBackDirection = mySelf.position - attackSource.position;
        knockBackDirection.Normalize();

        // Subit le recul de l'attaque
        myBody.velocity = knockBackDirection * knockbackSensitivity * knockbackPower + new Vector2(0, 10);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        redOverlay.reset();

        if (health != 0)
        {
            StartCoroutine(Vibrating(0.3f, 0.8f));
        }

        soundManager.Play("Mort");
    }

    void DetectController()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevetat.IsConnected)
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

        prevetat = state;
        state = GamePad.GetState(playerIndex);
    }

    IEnumerator Vibrating(float duration, float vibrationForce)
    {
        float time = 0f;

        while (duration > time)
        {
            time = time + Time.deltaTime;
            GamePad.SetVibration(playerIndex, vibrationForce, vibrationForce);
            yield return 0; 
        }

        GamePad.SetVibration(playerIndex, 0, 0);

        yield return null;
    }

    protected IEnumerator damageInvulnerabilty(float duration)
    {
        haveTakeDamage = true;

        yield return new WaitForSeconds(duration);

        haveTakeDamage = false;
    }
}