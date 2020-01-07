using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    SpriteRenderer SpriteRenderer = default;
    Image image = default;
    Color color = Color.white;
    float time = 0f;
    public float timeInSec = 2f;

    void Start()
    {
        SpriteRenderer = this.GetComponent<SpriteRenderer>();
        image = this.GetComponent<Image>();

        if(SpriteRenderer == null)
        {
            time = 0f;
        }
    }

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
        if(color.a > 0)
        {
            color.a = Mathf.Lerp( 1, 0, time);

            if(SpriteRenderer == null)
            {
                image.color = color;
            }
            else
            {
                SpriteRenderer.color = color;
            }
        }

    }

    public void reset()
    {
        time = 0;
        color.a = 1;
        if (SpriteRenderer == null)
        {
            image.color = color;
        }
        else
        {
            SpriteRenderer.color = color;
        }

    }

}
