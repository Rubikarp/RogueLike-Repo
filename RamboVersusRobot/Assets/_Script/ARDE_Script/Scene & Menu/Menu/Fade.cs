using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer SpriteRenderer = default;
    float time = 0f;
    public float timeInSec = 2f;

    void Start()
    { SpriteRenderer = this.GetComponent<SpriteRenderer>(); }

    void Update()
    {
        if (time < 1)
        {
            time += Time.deltaTime / timeInSec;
        }
        FadeTime(timeInSec);
    }

    void FadeTime(float timeInSec)
    {
        Color color;
        color = Color.white;

        if(color.a > 0)
        {
            color.a = 1 - time;
            SpriteRenderer.color = color;
        }
    }
}
