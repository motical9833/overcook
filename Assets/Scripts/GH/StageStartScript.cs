using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartScript : MonoBehaviour
{
    public GameObject[] readyGoUIObjects = new GameObject[2];

    void Awake()
    {
        for (int i = 0; i < readyGoUIObjects.Length; i++)
        {
            readyGoUIObjects[i] = this.transform.GetChild(i).gameObject;

            if (readyGoUIObjects[i] == null)
            {
                Debug.Log("ReadyUI or GoUI »ç¶óÁü");
            }
        }
    }


    IEnumerator StartUICoroutine()
    {
        readyGoUIObjects[0].SetActive(true);
        GameObject audioManager = GameObject.FindWithTag("AudioManager");
        AudioSource m_AudioSource = this.GetComponent<AudioSource>();

        m_AudioSource.clip = audioManager.GetComponent<AudioManager>().GetAudioClip("LevelReady");
        m_AudioSource.Play();
        
        yield return new WaitForSeconds(3);
        
        readyGoUIObjects[0].SetActive(false);
        readyGoUIObjects[1].SetActive(true);

        yield return new WaitForSeconds(1);

        readyGoUIObjects[0].SetActive(false);
        readyGoUIObjects[1].SetActive(false);

        audioManager.GetComponent<BGMScript>().StartBGM("Demo1v2", true);
    }

    public void ReadyGoUIOn()
    {
        StartCoroutine(StartUICoroutine());
    }
}
