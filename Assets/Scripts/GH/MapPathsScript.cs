using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class MapPathsScript : MonoBehaviour
{
    public float changeDuration = 1.0f;

    //스테이지를 로드할 때 활성화 시키늗 함수

    public void ActivePath(int procedure)
    {
        GameObject pathParent = this.transform.GetChild(procedure).gameObject;

        pathParent.SetActive(true);
    }

    //procedure(스테이지의 순서) 스테이지를 클리어 한 후 다음 스테이지로 연결하는 Path를 활성화 및
    //코루틴을 실행하는 함수
    public void UnLockPath(int procedure)
    {
        Transform pathsTr = this.transform.GetChild(procedure);
        pathsTr.gameObject.SetActive(true);

        for (int i = 0; i < pathsTr.childCount; i++)
        {
            StartCoroutine(PathScaleController(pathsTr.GetChild(i),changeDuration));
        }
    }

    //Path의 스케일값을 Lerp를 이용하여 조절하는 코루틴
    IEnumerator PathScaleController(Transform pathTr,float duration)
    {
        Vector3 initialScale = pathTr.localScale;

        float elapsedTime = 0.0f;

        while (elapsedTime < changeDuration)
        {
            pathTr.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
