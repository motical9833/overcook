using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSelectUIScript : MonoBehaviour
{
    private enum State
    { 
        CAMPAIGN,
        VERSUS,
        MORSEL,
        FESTIVE,
        OPTIONS,
        CREDITS,
        QUIT,
    }

    State state = State.CAMPAIGN;

    GameObject mySelectArrow;
    public bool isSelectMenu = false;
    public float moveDistance = 0.004f;
    public float coolTIme;

    void Start()
    {
        mySelectArrow = this.GetComponent<Transform>().GetChild(0).gameObject;
    }

    void Update()
    {
        if(isSelectMenu)
        {
            coolTIme += Time.deltaTime;

            HandleInput();
            HandleSelection();

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                DeActivateUIArrow();
                coolTIme = 0.0f;
            }
        }
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && state != State.QUIT)
        {
            ChangeState(1);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && state != State.CAMPAIGN)
        {
            ChangeState(-1);
        }
    }

    private void ChangeState(int direction)
    {
        state += direction;
        float moveY = direction * -moveDistance;
        mySelectArrow.transform.localPosition += new Vector3(0.0f, moveY, 0.0f);
    }

    private void HandleSelection()
    {
        if(Input.GetKeyDown(KeyCode.Space) && coolTIme >= 1.0f)
        {
            switch (state)
            {
                case State.CAMPAIGN:
                    //StartCoroutine(SelectUIEvent("StageSelectScene"));
                    LoadScene("StageSelectScene");
                    break;
                case State.VERSUS:
                    break;
                case State.MORSEL:
                    break;
                case State.FESTIVE:
                    break;
                case State.OPTIONS:
                    break;
                case State.CREDITS:
                    break;
                case State.QUIT:
                    break;
                default:
                    break;
            }
        }
    }

    //LoadScene에서 씬전환할 때 오디오 셀렉트 사운드가 들리지 않게 되면 사용해야함
    //추후 확인 후 변경 or 코드 제거
    private IEnumerator SelectUIEvent(string sceneName)
    {
        this.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.1f);

        LoadScene(sceneName);
    }

    private void LoadScene(string sceneName)
    {
        this.GetComponent<AudioSource>().Play();
        coolTIme = 0;
        isSelectMenu = false;
        ResetUIArrow();
        SceneManager.LoadScene(sceneName);
    }

    public void ActivateUIArrow()
    {
        ResetUIArrow();
        isSelectMenu = true;
    }

    public void DeActivateUIArrow()
    {
        isSelectMenu = false;
    }

    private Transform GetRootParent(Transform child)
    {
        while(child.parent != null)
        {
            child = child.parent;
        }

        return child;
    }

    private void ResetUIArrow()
    {
        state = 0;
        mySelectArrow.transform.localPosition = new Vector3(0.00922966f, 0.008f, -0.002710001f);
    }
}
