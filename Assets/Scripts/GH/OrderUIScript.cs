using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUIScript : MonoBehaviour
{
    // order 게이지
    Slider slider;
    public Image fill;

    Color greenColor = Color.green;
    Color redColor = Color.red;
    // 제한 시간
    private float initialTimer = 80.0f;

    // 현재 시간
    float currentTimer;
    float timeInterval;
    bool isDanger;

    AudioSource m_AudioSource;

    void Start()
    {
        currentTimer = initialTimer;
        slider = this.gameObject.GetComponent<Transform>().GetChild(2).GetComponent<Slider>();
        fill = slider.GetComponent<Transform>().GetChild(1).GetChild(0).GetComponent<Image>();

        fill.color = greenColor;

        m_AudioSource = this.GetComponent<AudioSource>();
        m_AudioSource.clip = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().GetAudioClip("LevelTimerBeep");
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;

        // 타이머 비율 계산 및 슬라이더 업데이트
        slider.value = currentTimer / initialTimer;

        // 색상 전환
        float t = Mathf.Clamp01(currentTimer / initialTimer);
        fill.color = Color.Lerp(redColor, greenColor, t);

        // 경고음 재생
        if (currentTimer <= 10.0f && !m_AudioSource.isPlaying)
        {
            m_AudioSource.Play();
        }

        // 타임종료
        if (currentTimer <= 0)
        {
            ResetTimer();
        }
    }

    public float GetCurrentTime()
    {
        return currentTimer;
    }

    public void ResetTimer()
    {
        currentTimer = initialTimer;
        slider.value = 1;
        fill.color = greenColor;
        isDanger = false;
        m_AudioSource.Stop();
    }
}