using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class GHMoveScript : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotSpeed = 5.0f;
    Vector3 move;
    bool isMove = true;
    ParticleSystem mParticleSystem;

    void Start()
    {
        mParticleSystem = this.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();

        if(!mParticleSystem)
        {
            Debug.Log("파티클 시스템을 가져오지 못했습니다.");
        }
    }

    void Update()
    {
        if (!isMove)
            return;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        bool isInput = vertical == 0 && horizontal == 0;

        if(mParticleSystem)
        {
            if (isInput && mParticleSystem.isPlaying)
            {
                mParticleSystem.Stop();
            }
            else if(!isInput && !mParticleSystem.isPlaying)
            {
                mParticleSystem.Play();
            }
        }


        if(vertical != 0)
        {
            this.transform.Translate(0.0f, 0.0f, vertical * moveSpeed * Time.deltaTime);
        }

        if (horizontal != 0)
        {
            transform.Rotate(Vector3.up * horizontal * rotSpeed * Time.deltaTime);
        }
    }


    public void IsMove(bool value)
    {
        isMove = value;
    }
}