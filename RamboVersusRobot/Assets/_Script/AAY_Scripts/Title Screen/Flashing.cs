using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour
{
    public GameObject flashing_Label;
    public float flashInterval;
    
    void Start()
    {
        InvokeRepeating("FlashLabel", 0, flashInterval);
    }

	void FlashLabel()
	{
        flashing_Label.SetActive(!flashing_Label.activeInHierarchy);
	}

}
