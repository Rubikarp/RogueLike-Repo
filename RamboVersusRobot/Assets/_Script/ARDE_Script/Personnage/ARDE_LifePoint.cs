using UnityEngine;

public class ARDE_LifePoint : MonoBehaviour
{
    public GameObject heart;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Quand le projectile entre en contact avec joueur, alors le joueur prend gagne  de l'energie
        if (other.CompareTag("Player"))
        {
            Destroy(heart);
        }

    }
}
