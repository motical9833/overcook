using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Compilation;
using UnityEngine;

public class StageTimerScript : MonoBehaviour
{
    float stageTimeLimit = 300.0f;
    float timeupTime = 5.0f;
    TextMeshProUGUI textMeshGUI = null;
    bool isStart = false;
    bool isTimeUP = false;
    public event Action EndTimeLimeted;

    StageSummaryControllerScript stageSummaryControllerScript;

    void Start()
    {
        textMeshGUI = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textMeshGUI.text = (stageTimeLimit / 60.0f).ToString() + ":" + (stageTimeLimit % 60.0f).ToString();
        stageSummaryControllerScript = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageSummaryControllerScript>();
    }

    void Update()
    {
        if (!isStart)
            return;

        stageTimeLimit -= Time.deltaTime;

        textMeshGUI.text = ((int)(stageTimeLimit / 60) + ":" + (int)(stageTimeLimit % 60)).ToString();

        if (stageTimeLimit < 0 && !isTimeUP)
        {
            isStart = false;
            isTimeUP = true;
            StartCoroutine(TimeUPUICoroutine());
        }
    }

    IEnumerator TimeUPUICoroutine()
    {
        BGMScript bgmScript = GameObject.FindWithTag("AudioManager").GetComponent<BGMScript>();
        if (!bgmScript)
        {
            Debug.Log("bgmScript가 존재하지 않음!");
        }
        GameObject timeUpPanal = this.transform.GetChild(2).gameObject;
        timeUpPanal.SetActive(true);
        timeUpPanal.GetComponent<TimeUPUIScript>().TimeUP();


        bgmScript.StartBGM("TimesUpSting",false);

        yield return new WaitForSeconds(timeupTime);

        OpenSummary(bgmScript);

        //EndTimeLimeted();
    }

    private void OpenSummary(BGMScript script)
    {

        if(!stageSummaryControllerScript)
        {
            Debug.Log("스크립트를 찾을 수 없음");
            return;
        }

        stageSummaryControllerScript.OpenUISummaryData();
        script.StartBGM("LevelVictorySound",false);
        this.gameObject.SetActive(false);

    }

    public void StartTimer()
    {
        isStart = true;
    }
}