using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    AudioManager audioManager = null;
    AudioSource audioSource = null;

    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioSource = gameObject.GetComponent<AudioSource>();

        if (!audioSource || !audioManager)
        {
            Debug.Log("오디오 매니저 또는 오디오 소스가 오브젝트에 존재하지 않습니다.");
            return;
        }
        audioSource.clip = audioManager.GetAudioClip("StartScreenMusic");
        audioSource.Play();
    }

    public void StartBGM(string name,bool isLoop)
    {
        audioSource.clip = audioManager.GetAudioClip(name);
        audioSource.Play();
        audioSource.loop = isLoop;
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}