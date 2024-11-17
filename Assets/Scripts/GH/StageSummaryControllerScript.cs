using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSummaryControllerScript : MonoBehaviour
{
    public class SummaryData
    {
        public int orderDelivered { get; set; }
        public int tips { get; set; }
        public int orderFailedCount { get; set; }

        public SummaryData(int delivered = 0, int failedCount = 0, int tip = 0)
        {
            orderDelivered = delivered;
            orderFailedCount = failedCount;
            tips = tip;
        }
    }

    SummaryData summaryData;
    public StageSummaryScript stageSummaryScript;
    GameObject mainCanvas;
    public GameObject mask;

    public void Awake()
    {
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        summaryData = new SummaryData();
        mask = GameObject.FindWithTag("Mask");
        stageSummaryScript = GameObject.FindWithTag("MainCanvas").transform.GetChild(4).GetComponent<StageSummaryScript>();

        if (!stageSummaryScript)
        {
            Debug.Log("stageSummaryScript를 찾을 수 없음");
            return;
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            OpenUISummaryData();
        }
    }

    public void OpenUISummaryData()
    {
        stageSummaryScript.gameObject.SetActive(true);
        // 점수 표시
        stageSummaryScript.SetSummaryTextUI(summaryData.orderDelivered, summaryData.tips, summaryData.orderFailedCount);
        // UI효과 실행
        mask.GetComponent<IrisInScript>().StartIrisInUI();
    }

    public SummaryData GetSummaryData(){ return summaryData; }
    public void SetOrderDelivered(int count){ summaryData.orderDelivered += count; }
    public void SetTips(int tip){ summaryData.tips += tip; }
    public void SetOrderFailedCount(int count) { summaryData.orderFailedCount += count; }
    public int GetOrderDelivered(){ return summaryData.orderDelivered; }
    public int GetTips(){ return summaryData.tips; }
    public int GetOrderFailedCount() { return summaryData.orderFailedCount; }
}