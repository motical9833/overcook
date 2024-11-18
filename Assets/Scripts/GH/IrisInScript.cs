using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IrisInScript : MonoBehaviour
{
    GameObject mainCanvas;
    Sprite sprite;

    public Material irisMaterial;
    
    private void Awake()
    {
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        irisMaterial = this.GetComponent<Image>().material;
    }

    public void StartIrisInUI()
    {
        StartCoroutine(LerpCoroutine(1.0f));
    }

    // 아리이스 인 전환 효과를 코루틴으로 구현
    private IEnumerator LerpCoroutine(float duration)
    {
        if (duration <= 0)
        {
            Debug.LogWarning("duration은 0보다 커야합니다.");
            yield break;
        }

        float progress = 0.0f;
        this.transform.localScale = Vector3.one;

        while (progress < 1.0f)
        {
            progress += Time.deltaTime / duration;
            irisMaterial.SetFloat("_Radius", progress);
            yield return null;
        }

        irisMaterial.SetFloat("_Radius", 1.0f);
    }
}