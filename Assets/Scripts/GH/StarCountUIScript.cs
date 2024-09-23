using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StarCountUIScript : MonoBehaviour
{
    public TextMeshProUGUI starUiPanal;

    public void ApplyStarCountUI(int count)
    {
        if(starUiPanal == null)
        {
            Debug.Log("starUIPanal이 적용되어 있지 않음");
        }

       starUiPanal.text = count.ToString();
    }
}