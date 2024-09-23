using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectSceneControllerScript : MonoBehaviour
{
    public GameObject mapObject;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;
    }

    public void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        mapObject = GameObject.FindGameObjectWithTag("MapObject");

        if (mapObject == null)
        {
            Debug.Log("MapObject가 존재하지 않음!");
            return;
        }

        mapObject.GetComponent<MapGridController>().InitializeObjectGroups();
        mapObject.GetComponent<MapPathsControllerScript>().InitializePathGrop();
    }
}
