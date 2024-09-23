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
    //public float duration = 1.0f;

    //private float radius;
    //private bool isClosing = false;
    
    private void Awake()
    {
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        irisMaterial = this.GetComponent<Image>().material;
    }

    public void StartIrisInUI()
    {
        //this.GetComponent<Image>().sprite = sprite;
        StartCoroutine(LerpCoroutine(1.0f));
    }

    private IEnumerator LerpCoroutine(float duration)
    {
        float radius = 0.0f;
        this.transform.localScale = Vector3.one;

        while (radius < duration)
        {
            radius += Time.deltaTime / duration;
            radius = Mathf.Clamp(radius, 0.0f, 1.0f);
            irisMaterial.SetFloat("_Radius", radius);
            yield return null;
        }
    }
}