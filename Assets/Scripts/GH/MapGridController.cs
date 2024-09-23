using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridController : MonoBehaviour
{
    //bool isInitialize = false;

    [System.Serializable]

    public class MapObjects
    {
        public List<GameObject> objects = new List<GameObject>();
    }

    public MapObjects[] objectGrop = new MapObjects[5];
    public List<bool> isOpenHex = new List<bool>();
    GameObject tiles;

    public void InitializeObjectGroups()
    {
        if (this.transform.childCount == 0 || this.transform.GetChild(0).childCount == 0)
        {
            Debug.Log("스크립트가 잘못된 위치에 있음");
            return;
        }

        GameObject gameManager = GameObject.FindWithTag("GameManager");

        if (gameManager == null)
        {
            Debug.Log("게임 매니저가 존재하지 않음!");
            return;
        }

        tiles = this.transform.GetChild(0).GetChild(2).gameObject;

        for (int i = 0; i < objectGrop.Length; i++)
        {
            objectGrop[i] = new MapObjects();
        }

        //추후 확장할 때 수정 필요 [3]값
        for (int i = 0; i < 3; i++)
        {
            Transform gropTr = tiles.transform.GetChild(0).GetChild(i);
            int count = gropTr.childCount;

            for (int j = 0; j < count; j++)
            {
                objectGrop[i].objects.Add(gropTr.GetChild(j).gameObject);
            }
        }

        InitializeStageHex(gameManager);

        //isInitialize = true;
    }

    void InitializeStageHex(GameObject manager)
    {
        isOpenHex = manager.GetComponent<StageSaveLoadScript>().GetAllisAble();

        for (int i = 0;i < isOpenHex.Count;i++)
        {
            if (isOpenHex[i])
            {
                for (int j = 0; j < objectGrop[i].objects.Count; j++)
                {
                    objectGrop[i].objects[j].transform.localRotation = Quaternion.identity;
                }
            }
        }
    }

    public void TileFlipping(int level)
    {
        StartCoroutine(SelectTile(objectGrop[level].objects));
    }

    IEnumerator SelectTile(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            StartCoroutine(TileFlippingEnumerator(objects[i]));
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator TileFlippingEnumerator(GameObject obj)
    {
        Quaternion startRotation = obj.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(obj.transform.eulerAngles + new Vector3(180.0f, 0, 0));

        float elapsedTime = 0f;
        float duration = 1.0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            obj.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        obj.transform.rotation = endRotation;
    }
}
