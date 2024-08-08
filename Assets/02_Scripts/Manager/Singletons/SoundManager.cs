using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource PlayAtPoint(AudioClip audioClip, Vector3 position)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        gameObject.name = "TempAudioObject";
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(gameObject, audioClip.length);

        return audioSource;
    }


}
