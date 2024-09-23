using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSchedulerScript : MonoBehaviour
{
    public string csvFilePath = "Assets/Resources/StageData/StageOrderData.csv";
    private Dictionary<string, List<string>> order;
    GameObject gameManager;

    private void Awake()
    {
        LoadCSVData();
    }

    private void LoadCSVData()
    {
        order = new Dictionary<string, List<string>>();

        var orderDataList = CSVLoader.LoadCSV<StageOrderData>(csvFilePath, ConvertToOrderData);
            
        foreach (var data in orderDataList)
        {
            order[data.stageLevel] = data.Orders;
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
