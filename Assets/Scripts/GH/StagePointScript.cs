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
        point = value;

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
