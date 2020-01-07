using UnityEngine;
using System.Collections;

public class Flashing : MonoBehaviour
{
    public GameObject flashing_Label;
    public float flashInterval;
    public float timeToWait = 2f;

    void Start()
    {
        flashing_Label.SetActive(false);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeToWait);

        InvokeRepeating("FlashLabel", 0, flashInterval);

        yield return null;
    }

    void FlashLabel()
	{
        flashing_Label.SetActive(!flashing_Label.activeInHierarchy);
	}

}
