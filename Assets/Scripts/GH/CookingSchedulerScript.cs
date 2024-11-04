using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CookingSchedulerScript : MonoBehaviour
{
    public string csvFileName = "StageOrderData.csv";
    private Dictionary<string, List<string>> order;
    private Dictionary<string, List<string>> order_2;
    GameObject gameManager;

    private void Awake()
    {
        LoadCSVData();
    }

    private void LoadCSVData()
    {
        order = new Dictionary<string, List<string>>();

        string csvFilePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        //var orderDataList = CSVLoader.LoadCSV<StageOrderData>(csvFilePath, ConvertToOrderData);

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
                    Debug.Log("키가 이미 존재함");
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
            Debug.LogError("유효하지 않은 데이터");
            return null;
        }
    }

    public List<string> GetOrdersData(string level)
    {
        if(order.TryGetValue(level, out List<string> orders))
        {
            return orders;
        }
        else
        {
            Debug.Log("데이터가 존재하지 않음");
            return null;
        }
    }
}
