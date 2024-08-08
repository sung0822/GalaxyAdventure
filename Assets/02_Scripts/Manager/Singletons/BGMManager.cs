using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : Singleton<BGMManager>
{

    [SerializeField] AudioSource previousAudioSource;
    [SerializeField] AudioSource currentAudioSource;
    [SerializeField] AudioSource nextAudioSource;
    public List<AudioClip> bgmList { get { return _bgmList; } set { _bgmList = value; } }
    [SerializeField] private List<AudioClip> _bgmList = new List<AudioClip>();

    [SerializeField] public AudioClip bossBgm;
    
    public float changingTime { get { return _changingTime; } set { SetChangingTime(value); } }
    [SerializeField] private float _changingTime = 1;


    private void Start()
    {
        SetChangingTime(_changingTime);
    }

    Coroutine currentPlayingCoroutine;
    public void StartBGM()
    {
        currentPlayingCoroutine = StartCoroutine(PlayBGM());
    }
    private IEnumerator PlayBGM()
    {
        while (true)
        {
            for (int i = 0; i < bgmList.Count; i++)
            {
                if (previousAudioSource != null)
                    Destroy(previousAudioSource);

                nextAudioSource = gameObject.AddComponent<AudioSource>();
                nextAudioSource.clip = bgmList[i];
                nextAudioSource.volume = 0;
                nextAudioSource.Play();
                
                yield return StartCoroutine(DissolveBGM(bgmList[i]));
                yield return WaitForClipLength(currentAudioSource.clip);
            }
        }
    }
    public void ChangeBGM(AudioClip audioClip, bool loop = true)
    {
        if (previousAudioSource != null)
        {
            Destroy(previousAudioSource);
        }
        StopCoroutine(currentPlayingCoroutine);
        nextAudioSource = gameObject.AddComponent<AudioSource>();
        nextAudioSource.clip = audioClip;
        nextAudioSource.loop = true;
        nextAudioSource.volume = 0;
        nextAudioSource.Play();
        
        StartCoroutine(DissolveBGM(audioClip));
    }

    IEnumerator DissolveBGM(AudioClip audioClip)
    {
        while (true) 
        {
            currentAudioSource.volume -= (1 / changingTime) * Time.deltaTime;
            nextAudioSource.volume += (1 / changingTime) * Time.deltaTime;

            if ((nextAudioSource.volume >= 1) && (currentAudioSource.volume <= 0))
            {
                currentAudioSource.volume = 0;
                nextAudioSource.volume = 1;
                
                previousAudioSource = currentAudioSource;
                currentAudioSource = nextAudioSource;
                nextAudioSource = null;
                Debug.Log("전환 완료");
                break;
            }

            yield return new WaitForEndOfFrame();

        }
    }
    void SetChangingTime(float changingTime)
    {
        this._changingTime = 1 / changingTime;
        this._changingTime = changingTime;
    }

    IEnumerator WaitForClipLength(AudioClip audioClip)
    {
        yield return new WaitForSeconds(currentAudioSource.clip.length - changingTime);
    }
}
