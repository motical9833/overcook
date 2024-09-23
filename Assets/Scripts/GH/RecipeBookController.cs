using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class RecipeBookController : MonoBehaviour
{
    Transform[] childs = new Transform[5];
    private Camera myCamera;
    private float currentTime = 0.0f;
    private bool isMoveing = false;
    private bool islookCloser = false;
    public float moveDuration = 1.0f;
    public Vector3[] cameraPos;
    public Vector3[] cameraRot;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            childs[i] = gameObject.transform.GetChild(i);
        }

        myCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            this.GetComponent<BookCoverScript>().GetIsOpening() &&
            islookCloser == false && !isMoveing)
        {
            this.GetComponent<BookCoverScript>().CloseCover();
            isMoveing = true;
            islookCloser = true;
        }

        if(isMoveing)
        {
            currentTime += Time.deltaTime;
            float time = Mathf.Clamp01(currentTime / moveDuration);

            Vector3 startPos = islookCloser ? cameraPos[1] : cameraPos[0];
            Vector3 endPos = islookCloser ? cameraPos[0] : cameraPos[1];

            Quaternion startQuaternion = islookCloser ? Quaternion.Euler(cameraRot[1]) : Quaternion.Euler(cameraRot[0]);
            Quaternion endQuaternion = islookCloser ? Quaternion.Euler(cameraRot[0]) : Quaternion.Euler(cameraRot[1]);

            myCamera.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            myCamera.transform.localRotation = Quaternion.Lerp(startQuaternion,endQuaternion, time);


            if (time >= 1.0f)
            {
                isMoveing = false;
                currentTime = 0;
            }
        }
    }

    public bool GetIsLookCloser(){ return islookCloser; }

    public void MovingCamera()
    {
        isMoveing = true;
        islookCloser = false;
    }

    public void ResetCamera()
    {
        isMoveing = true;
        islookCloser = true;
    }
}
