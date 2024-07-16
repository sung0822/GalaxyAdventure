using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    public AudioSource PlayAtPoint(AudioClip audioClip, Vector3 position)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        gameObject.name = "TempAudioObject";
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = audioClip;

        Destroy(gameObject, audioClip.length);

        return audioSource;
    }


}
