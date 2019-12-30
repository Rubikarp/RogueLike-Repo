using UnityEngine;
using System;
using UnityEngine.Audio;


public class ARDE_SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    //FindObjectOfType<ARDE_SoundManager>().Play("name");

    void Awake()
    {
        foreach(Sound son in sounds)
        {
            son.source = gameObject.AddComponent<AudioSource>();
            son.source.clip = son.clip;

            son.source.volume = son.volume;
            son.source.pitch = son.pitch;
            son.source.loop = son.loop;
        }
    }

    void Play(string name)
    {
        Sound son = Array.Find(sounds, Sound => Sound.name == name);
        if(son == null)
        {
            Debug.Log(name + "est introuvable, vérifier qu'il soit correctement écris");
            return;
        }
    }
}
