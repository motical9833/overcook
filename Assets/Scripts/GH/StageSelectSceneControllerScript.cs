using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectSceneControllerScript : MonoBehaviour
{
    public GameObject mapObject;

    private void Start()
    {
        SceneManager.sceneLoaded += StageSelectSceneLoadInitialize;
    }

    // StageSelectScene을 로드할 때 Gird와 MpaPath의 상태를 세팅하는 함수
    public void StageSelectSceneLoadInitialize(Scene scene, LoadSceneMode mode)
    {
        mapObject = GameObject.FindGameObjectWithTag("MapObject");

        if (mapObject == null)
        {
#if UNITY_EDITOR
            Debug.Log("MapObject가 존재하지 않음!");
#endif
            return;
        }

        // 저장된 MapGrid를 바탕으로 초기화 하는 함수
        mapObject.GetComponent<MapGridController>().InitializeObjectGroups();

        // 저장된 MpaPath를 바탕으로 초기화 하는 함수
        mapObject.GetComponent<MapPathsControllerScript>().InitializePathGrop();
    }
}