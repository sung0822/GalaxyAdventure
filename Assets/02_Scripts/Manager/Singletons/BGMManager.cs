using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance = null;

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


    [SerializeField] AudioSource freeAudioSource;
    [SerializeField] AudioSource currentAudioSource;
    [SerializeField] AudioSource nextAudioSource;

    [SerializeField] public AudioClip bgm1;
    [SerializeField] public AudioClip bgm2;
    [SerializeField] public AudioClip bgm3;
    [SerializeField] public AudioClip bgm4;

    [SerializeField] public AudioClip bossBgm5;

    [SerializeField] private float changingTime { get { return _changingTime; } set { SetChangingTime(value); } }
    private float _changingTime = 1;
    private float changingTimeForFrame;


    private void Start()
    {
        SetChangingTime(_changingTime);
    }

    public void PlayBGM(AudioClip audioClip)
    {
        currentAudioSource.Stop();
        currentAudioSource.clip = audioClip;
        currentAudioSource.Play();
        Debug.Log("브금 오디오 호출");
    }

    public void ChangeBGM(AudioClip audioClip)
    {
        nextAudioSource.clip = audioClip;
        nextAudioSource.volume = 0;
        StartCoroutine(DissolveBGM(audioClip));
    }

    IEnumerator DissolveBGM(AudioClip audioClip)
    {
        while (true) 
        {
            currentAudioSource.volume -= changingTimeForFrame * Time.deltaTime;
            nextAudioSource.volume += changingTimeForFrame * Time.deltaTime;

            if ((nextAudioSource.volume >= 1) && (currentAudioSource.volume <= 0))
            {
                
                currentAudioSource.volume = 0;
                nextAudioSource.volume = 1;
                
                freeAudioSource = currentAudioSource;
                currentAudioSource = nextAudioSource;
                nextAudioSource = freeAudioSource;

            }


            yield return new WaitForEndOfFrame();

        }
    }

    void SetChangingTime(float changingTime)
    {
        changingTimeForFrame = 1 / changingTime;
        this._changingTime = changingTime;
    }


}
