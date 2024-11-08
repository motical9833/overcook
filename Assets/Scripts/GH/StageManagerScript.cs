using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour
{
    public OrderUIControllerScript orderUIControllerScript;
    public StageTimerScript stageTimerScript;
    public StagePointScript stagePointScript;
    public StageStartScript stageStartScript;
    public StageSaveLoadScript stageSaveLoadScript;
    public List<PotScript> potScripts;
    public List<GameObject> players;

    public GameObject mainCanvas;


    void Start()
    {
        stageSaveLoadScript = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<StageSaveLoadScript>();
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").gameObject;

        GameObject[] pots = GameObject.FindGameObjectsWithTag("Pot");

        for (int i = 0; i < pots.Length; i++)
        {
            potScripts.Add(pots[i].GetComponent<PotScript>());
        }

        GameObject[] playerArr = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playerArr.Length; i++)
        {
            players.Add(playerArr[i]);
        }

        if (!stageSaveLoadScript || !mainCanvas)
        {
#if UNITY_EDITOR
            Debug.Log("stageSaveLoadScript 스크립트를 찾을 수 없음");
#endif
            return;
        }

        stagePointScript = mainCanvas.transform.GetChild(1).GetComponent<StagePointScript>();
        stageTimerScript = mainCanvas.transform.GetChild(2).GetComponent<StageTimerScript>();
        stageStartScript = mainCanvas.transform.GetChild(3).GetComponent<StageStartScript>();

        orderUIControllerScript = this.GetComponent<OrderUIControllerScript>();

        StartCoroutine(GameStartCoroutine());
    }

    // 게임을 클리어 하였을 때의 점수들을 GameManager에 전달하고
    // 씬을 로드하였을 때 이벤트를 추가하는 함수
    public void GameClear()
    {
        string name = SceneManager.GetActiveScene().name;
        int score = this.GetComponent<StageSummaryControllerScript>().GetOrderDelivered() * 20;

        StageInfo info = stageSaveLoadScript.GetPrevStageInfo();

        info.stageName = name;
        info.score = score;
        info.isCleared = true;
        info.isAble = true;

        stageSaveLoadScript.SavePrevStageInfo(info);

        stagePointScript.ResetPoint();

        SceneLoadEvent();
    }
    
    // 씬이 로드될때 실행되는 이벤트 함수
    private void SceneLoadEvent()
    {
        SceneManager.LoadSceneAsync("StageSelectScene");

        SceneManager.sceneLoaded += SaveClearDataAndLoadScene;
        stageSaveLoadScript.StageToSceneLoad();
    }

    public void GameFailed()
    {
        stagePointScript.ResetPoint();

        SceneManager.LoadSceneAsync("StageSelectScene");

        SceneManager.sceneLoaded += SaveClearDataAndLoadScene;
        stageSaveLoadScript.StageToSceneLoad();
    }

    void StageInfoDeliver(string name,StageInfo info)
    {
        GameObject.Find(name).GetComponent<Stage>().SaveStageData(info);
    }

    // StageSelectScene이 로드될때 필요한 데이터를 전달하고 씬을 로드하는 함수
    public void SaveClearDataAndLoadScene(Scene scene, LoadSceneMode mode)
    {
        StageInfo info = stageSaveLoadScript.GetPrevStageInfo();

        string name = info.stageName;

        GameObject stage = GameObject.Find(name);

        GameObject mapObject = GameObject.FindWithTag("MapObject");

        GameObject audioManager = GameObject.FindWithTag("AudioManager");

        // StageSelectScene의 StageObject에 데이터를 전달하는 함수
        stage.GetComponent<Stage>().SaveStageData(info);

        // 전체 Star의 갯수를 가져오는 함수
        int count = info.GetStarCount();

        // 클리어된 스테이지들의 Star 갯수를 적용 시키는 함수
        stage.GetComponent<StarLevelUIControllerScript>().SetStarImageBasedOnCount(count);

        // 잠겨져 있는 스테이지를 오픈하는 함수
        GameObject.FindWithTag("StageObject").GetComponent<StageObjectsControllerScript>().OpenStage();

        int procedure = stage.GetComponent<Stage>().GetStageInfo().procedure;

        // Scene이 로드된 후 Tile과 Path에 이벤트를 실행 시키는 함수
        mapObject.GetComponent<MapGridController>().TileFlipping(procedure + 1);
        mapObject.GetComponent<MapPathsControllerScript>().OpenPath(procedure);

        audioManager.GetComponent<BGMScript>().StartBGM("StartScreenMusic",true);

        SceneManager.sceneLoaded -= SaveClearDataAndLoadScene;
    }

    IEnumerator GameStartCoroutine()
    {
        stageStartScript.ReadyGoUIOn();

        yield return new WaitForSeconds(4);

        orderUIControllerScript.OrderStart();
        stageTimerScript.StartTimer();
    }

    public void StageScriptStop()
    {
        for (int i = 0; i < potScripts.Count; i++)
        {
            potScripts[i].PotScriptStop();
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerCtrlScript>().SetPlayerStop(true);
        }
    }

    public void StageScriptPlay()
    {
        for (int i = 0; i < potScripts.Count; i++)
        {
            potScripts[i].PotScriptPlay();
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerCtrlScript>().SetPlayerStop(false);
        }
    }
}