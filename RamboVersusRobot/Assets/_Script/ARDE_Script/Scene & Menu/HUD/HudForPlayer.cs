using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudForPlayer : MonoBehaviour
{
    //CharacterLifeSystem playerLife = null;

    [SerializeField] Image[] coeur = null;
    [SerializeField] Slider energieBar = null;

    [Range(0, 7)]   public int live = 5;
    [Range(0, 100)] public int NRG = 80;



    void Update()
    {
        //lifeBarUpdate(playerLife.health);
        lifeBarUpdate(live);

        EnergieJauge(NRG);
    }


    private void lifeBarUpdate(int health)
    {
        switch (health)
        {
            case 0:
                {
                    foreach(Image allImage in coeur)
                    {
                        allImage.gameObject.SetActive(false);
                    }
                }
                break;
            case 1:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(false);
                    coeur[2].gameObject.SetActive(false);
                    coeur[3].gameObject.SetActive(false);
                    coeur[4].gameObject.SetActive(false);
                    coeur[5].gameObject.SetActive(false);
                    coeur[6].gameObject.SetActive(false);

                }
                break;
            case 2:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(true);
                    coeur[2].gameObject.SetActive(false);
                    coeur[3].gameObject.SetActive(false);
                    coeur[4].gameObject.SetActive(false);
                    coeur[5].gameObject.SetActive(false);
                    coeur[6].gameObject.SetActive(false);
                }
                break;
            case 3:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(true);
                    coeur[2].gameObject.SetActive(true);
                    coeur[3].gameObject.SetActive(false);
                    coeur[4].gameObject.SetActive(false);
                    coeur[5].gameObject.SetActive(false);
                    coeur[6].gameObject.SetActive(false);
                }
                break;
            case 4:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(true);
                    coeur[2].gameObject.SetActive(true);
                    coeur[3].gameObject.SetActive(true);
                    coeur[4].gameObject.SetActive(false);
                    coeur[5].gameObject.SetActive(false);
                    coeur[6].gameObject.SetActive(false);
                }
                break;
            case 5:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(true);
                    coeur[2].gameObject.SetActive(true);
                    coeur[3].gameObject.SetActive(true);
                    coeur[4].gameObject.SetActive(true);
                    coeur[5].gameObject.SetActive(false);
                    coeur[6].gameObject.SetActive(false);
                }
                break;
            case 6:
                {
                    coeur[0].gameObject.SetActive(true);
                    coeur[1].gameObject.SetActive(true);
                    coeur[2].gameObject.SetActive(true);
                    coeur[3].gameObject.SetActive(true);
                    coeur[4].gameObject.SetActive(true);
                    coeur[5].gameObject.SetActive(true);
                    coeur[6].gameObject.SetActive(false);
                }
                break;
            case 7:
                {
                    foreach (Image allImage in coeur)
                    {
                        allImage.gameObject.SetActive(true);
                    }
                }
                break;

            default:
                {
                    Debug.Log("Problème avec l'affichage de la vie");
                }
                break;
        }

    }

    private void EnergieJauge(int energie)
    {
        energieBar.value = energie;
    }
}
