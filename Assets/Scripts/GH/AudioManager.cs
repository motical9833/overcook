using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSoure;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioSoure = GetComponent<AudioSource>();

        if (!audioSoure)
        {
            Debug.Log("오디오 소스가 존재하지 않음!!");
            return;
        }

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sound/Audio");

        for (int i = 0; i < clips.Length; i++)
        {
            audioClips.Add(clips[i].name, clips[i]);
        }
    }


    public AudioClip GetAudioClip(string name)
    {
        AudioClip clip = audioClips[name];

        if (!clip)
        {
            Debug.Log("해당 오디오 클립이 존재하지 않습니다.");
            return null;
        }
        return clip;
    }
}