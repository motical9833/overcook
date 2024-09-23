using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class BookCoverScript : MonoBehaviour
{
    public Transform bookCoverTr;
    public GameObject openBookButton;
    private bool isTurning = false;
    private bool isOpening = false;
    private float turnDuration = 1.0f;
    private float currentTime = 0;

    void Start()
    {
        this.bookCoverTr = transform.GetChild(3);
        openBookButton = GameObject.Find("ScreenCanvas").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (isTurning)
        {
            currentTime += Time.deltaTime;
            float time = Mathf.Clamp01(currentTime / turnDuration);

            float startAngle = isOpening ? -180 : 0;
            float endAngle = isOpening ? 0 : -180;

            float angle = Mathf.Lerp(startAngle, endAngle, time);
            bookCoverTr.localRotation = Quaternion.Euler(angle, -90, 0);

            if (time >= 1.0f)
            {
                isTurning = false;
                currentTime = 0;

                if(!isOpening)
                    openBookButton.SetActive(true);
            }
        }
    }

    public void OpenCover()
    {
        isTurning = true;
        currentTime = 0;
        isOpening = true;
        openBookButton.SetActive(false);
    }

    public void CloseCover()
    {
        isTurning = true;
        currentTime = 0;
        isOpening = false;
    }

    public bool GetIsTurning() { return isTurning; }
    public bool GetIsOpening() { return isOpening; }
}
