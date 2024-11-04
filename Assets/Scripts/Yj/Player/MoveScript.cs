using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveScript : MonoBehaviour
{
    GameObject model;
    float moveSpeed;
    float rotSpeed;

    float dashAccMaxSpeed = 7.00f;
    float dashAccSpeed = 0.0f;

    bool isDashing;
    bool isDashCool;
    //bool isReachMaxSpeed = false;

    AudioSource audiosource;

    public void SetInitial(GameObject _model, float _moveSpeed, float _rotSpeed)
    {
        model = _model;
        moveSpeed = _moveSpeed;
        rotSpeed = _rotSpeed;
        audiosource = this.GetComponent<AudioSource>();
    }
    private void Update()
    {
       
    }

    public IEnumerator Dash()
    {
        if(isDashing)
        {
            yield return null;
        }
        audiosource.Play();
        dashAccSpeed = dashAccMaxSpeed;
        yield return new WaitForSeconds(0.5f);
        dashAccSpeed = 0.0f;
    }

 
    public void Move(float h, float v)
    {
        transform.Translate(h * (moveSpeed + dashAccSpeed) * Time.deltaTime, 0.0f,
                         v * (moveSpeed + dashAccSpeed) * Time.deltaTime);

        Vector3 moveDirection = new Vector3(h, 0f, v).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }
}