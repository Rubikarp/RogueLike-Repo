using UnityEngine;
using System;
using UnityEngine.Audio;

public class ARDE_SoundManager : MonoBehaviour
{
    [SerializeField] public Sound[] sounds;

    /// <summary>
    /// public ARDE_SoundManager soundManager = default;
    /// 
    /// soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<ARDE_SoundManager>();
    /// 
    /// FindObjectOfType<ARDE_SoundManager>().Play("name");
    /// 
    /// </summary>

    void Awake()
    {
        foreach (Sound son in sounds)
        {
            son.source = gameObject.AddComponent<AudioSource>();
            son.source.clip = son.clip;

            son.source.volume = son.volume;
            son.source.pitch = son.pitch;
            son.source.loop = son.loop;
        }
    }

    void Update()
    {
        foreach (Sound son in sounds)
        {
            son.source.volume = son.volume;
            son.source.pitch = son.pitch;
            son.source.loop = son.loop;
        }
    }

    public void Play(string name)
    {
        Debug.Log(name);

        Sound son = Array.Find(sounds, sound => sound.name == name);
        if(son == null)
        {
            Debug.Log(name + "est introuvable, vérifier qu'il soit correctement écris");
            return;
        }
        else
        {
            son.source.Play();
        }
    }
}
