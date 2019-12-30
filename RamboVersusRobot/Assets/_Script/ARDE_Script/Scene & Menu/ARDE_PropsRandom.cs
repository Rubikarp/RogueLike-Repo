using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_PropsRandom : MonoBehaviour
{
    [HideInInspector]
    Transform me;

    //liste des éléments qui peuvent apparraitre
    [SerializeField] 
    GameObject[] objects;

    void Start()
    {
        me = this.transform;

        //je prends un chiffre random pour décider quel object va apparaitre
        int rand = Random.Range(0, objects.Length);

        //invocations de l'object
        Instantiate(objects[rand], me);
    }
}
