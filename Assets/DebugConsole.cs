using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    public TextMeshProUGUI consoleText; // UI Text 오브젝트
    private Queue<string> logMessages = new Queue<string>(); // 로그 메시지를 저장할 큐
    private const int maxMessages = 20; // 콘솔에 표시할 최대 메시지 수

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logMessages.Count >= maxMessages)
            logMessages.Dequeue(); // 오래된 메시지 제거
        logMessages.Enqueue(logString); // 새로운 메시지 추가

        consoleText.text = string.Join("\n", logMessages); // 콘솔 텍스트 업데이트
    }
}
