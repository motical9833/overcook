using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Compilation;
using UnityEngine;

public class StageTimerScript : MonoBehaviour
{
    float stageTimeLimit = 3.0f;
    float timeupTime = 5.0f;
    TextMeshProUGUI textMeshGUI = null;
    bool isStart = false;
    bool isTimeUP = false;
    //public event Action EndTimeLimeted;

    StageSummaryControllerScript stageSummaryControllerScript;
    public StageManagerScript stageManager;

    void Start()
    {
        textMeshGUI = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textMeshGUI.text = (stageTimeLimit / 60.0f).ToString() + ":" + (stageTimeLimit % 60.0f).ToString();
        stageSummaryControllerScript = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageSummaryControllerScript>();
        stageManager = GameObject.FindWithTag("StageManager").GetComponent<StageManagerScript>();
    }

    void Update()
    {
        if (!isStart)
            return;

        stageTimeLimit -= Time.deltaTime;
        UpdateTimerUI();

        if (stageTimeLimit < 0 && !isTimeUP)
        {
            TimeUP();
        }
    }

    // 타이머 UI 업데이트
    private void UpdateTimerUI()
    {
        int minutes = (int)(stageTimeLimit / 60);
        int seconds = (int)(stageTimeLimit % 60);
        textMeshGUI.text = $"{minutes}:{seconds}";
    }
    
    // 시간이 종료되었을 때 실행되는 메서드
    private void TimeUP()
    {
        isStart = false;
        isTimeUP = true;

        StartCoroutine(TimeUPUICoroutine());
    }

    // 게임이 끝났을 때 처리해야 할 메서드들을 실행시키는 코루틴
    IEnumerator TimeUPUICoroutine()
    {
        BGMScript bgmScript =
            GameObject.FindWithTag("AudioManager").GetComponent<BGMScript>();

        if (!bgmScript)
        {
            Debug.Log("bgmScript가 존재하지 않음!");
        }

        //
        GameObject timeUpPanal = this.transform.GetChild(2).gameObject;
        timeUpPanal.SetActive(true);
        // 타임업 UI 코루틴 실행
        timeUpPanal.GetComponent<TimeUPUIScript>().TimeUP();

        stageManager.StageScriptStop();

        bgmScript.StartBGM("TimesUpSting",false);

        // timeupTime만큼 지연
        yield return new WaitForSeconds(timeupTime);

        // 결과창 오픈
        OpenSummary(bgmScript);
    }

    // SummaryUI를 활성화 하고 UI 효과를 실행시키는 메서드
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