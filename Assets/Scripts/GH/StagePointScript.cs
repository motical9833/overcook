using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagePointScript : MonoBehaviour
{

    int point = 0;
    TextMeshProUGUI textMeshProGUI;

    void Start()
    {
        textMeshProGUI = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetPoint(int value)
    {
        Debug.Log("현재 포인트 : " + point);
        Debug.Log("vaule :" + value);

        point += value;
        Debug.Log("더해진 포인트 : " + point);

        textMeshProGUI.text = point.ToString();
    }

    public int GetPoint()
    {
        return point;
    }

    public void ResetPoint()
    {
        point = 0;
    }
}
