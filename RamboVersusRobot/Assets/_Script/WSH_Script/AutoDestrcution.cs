using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestrcution : MonoBehaviour
{

    private float timeSinceInitialization;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I'm here");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInitialization += Time.deltaTime;

        if (timeSinceInitialization >= 0.25)
        {

            GameObject.Destroy(gameObject);

        }

        



    }
}
