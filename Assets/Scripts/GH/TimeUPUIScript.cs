using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUPUIScript : MonoBehaviour
{
    public void TimeUP()
    {
        StartCoroutine(TimeUPCoroutine(this.transform,Vector3.zero,Vector3.one, 0.5f));
    }

    // 타임업 UI가 점점 커지는 코루틴
    IEnumerator TimeUPCoroutine(Transform m_tr, Vector3 startScale, Vector3 endSacle, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            m_tr.localScale =
                Vector3.Lerp(startScale, endSacle, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
