using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceGuageUIScript : MonoBehaviour
{
    private Transform targetCamTr;

    private Canvas canvas;
    public Slider sliderUi;


    private void Start()
    {
        targetCamTr = Camera.main.transform;
        sliderUi.minValue = 0.0f;
        sliderUi.maxValue = 100.0f;
        sliderUi.value = 0.0f;
    }

    void RookCamera()
    {
        //x축만 바뀌어야함
      /*  Vector3 directionToCamera = targetCamTr.position - transform.position;
        directionToCamera.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = targetRotation;*/
    }

    public void ShowSliceUI(float guage)
    {
        sliderUi.gameObject.SetActive(true);
        sliderUi.value = guage;
    }

    public void DisableSliceUI()
    {
        sliderUi.gameObject.SetActive(false);
    }

    // 카메라에 맞게 각도 맞춰주기..
    // slice가 진행 중일때만 Ui를 보여주기

}
