using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
public class StageSelectUIScript : MonoBehaviour
{
    private GameObject myStateUI;
    public GameObject[] startTexts = new GameObject[3];
    private GameObject player;
    private bool isSelect = false;

    private void Start()
    {
        myStateUI = gameObject.transform.GetChild(0).gameObject;

        for (int i = 0; i < startTexts.Length; i++)
        {
            startTexts[i] = myStateUI.transform.GetChild(i + 3).GetChild(2).gameObject;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // 선택된 스테이지에 따라 UI 변경 메서드
    public void SelectStage(Sprite img,string stageName , StageInfo stageData)
    {
        if (isSelect)
            return;

        myStateUI.SetActive(true);
        // 선택된 스테이지에 따라 이미지 변경
        myStateUI.transform.GetChild(1).GetComponent<Image>().sprite = img;

        // 선택된 스테이지에 따라 스테이지의 이름 변경
        string name = stageName.Replace("_", " ");
        myStateUI.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;


        // 스테이지별 목표점수에 따라 텍스트 변경
        for (int i = 0; i < startTexts.Length; i++)
        {
            startTexts[i].GetComponent<TextMeshProUGUI>().text = stageData.goals[i].ToString();
        }

        isSelect = true;
    }

    // 스테이지 UI화면에서 벗어나는 메서드
    public void ExitStageSelect()
    {
        if (!isSelect)
            return;

        myStateUI.SetActive(false);
        isSelect = false;
    }

    // 선택된 스테이지에 맞는 Scene으로 이동
    public void StartStage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        GameObject audioManager = GameObject.FindWithTag("AudioManager");
        audioManager.GetComponent<BGMScript>().StopBGM();
    }
}