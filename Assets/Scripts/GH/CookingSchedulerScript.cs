using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CookingSchedulerScript : MonoBehaviour
{
    public string csvFileName = "StageOrderData.csv";
    private Dictionary<string, List<string>> order;
    GameObject gameManager;

    private void Awake()
    {
        OrderLoad();
    }

    // 스테이지 주문 리스트 읽어오는 함수
    private void OrderLoad()
    {
        order = new Dictionary<string, List<string>>();
        // 파일 주소
        string csvFilePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("OrderLoad에서 csv파일을 읽어오지 못했습니다.");
        }

        // 읽어온 데이터
        var result = CSVReader.ParseCSV(File.ReadAllText(csvFilePath));

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i].Count > 1)
            {
                string key = result[i][0];
                List<string> values = result[i].GetRange(1, result[i].Count - 1);

                if(!order.ContainsKey(key))
                {
                    order.Add(key, values);
                }
                else
                {
                    Debug.Log("중복된 키가 존재했습니다.");
                }
            }
            else
            {
                Debug.Log("키에 맞는 List<string>의 길이가 1보다 작음");
            }
        }
    }

    private StageOrderData ConvertToOrderData(string[] values)
    {
        if (values.Length == 4)
        {
            return new StageOrderData
            {
                stageLevel = values[0],
                Orders = new List<string> { values[1], values[2], values[3] }
            };
        }
        else
        {
            Debug.LogError("ConvertToOrderData의 유효하지 않은 데이터가 들어왔음");
            return null;
        }
    }

    // 저장된 주문 리스트를 반환하는 함수
    public List<string> GetOrdersData(string level)
    {
        if(order.TryGetValue(level, out List<string> orders))
        {
            return orders;
        }
        else
        {
            Debug.Log("OrderData가 존재하지 않음");
            return null;
        }
    }
}
