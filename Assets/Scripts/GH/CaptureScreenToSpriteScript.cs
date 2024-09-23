using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CaptureScreenToSpriteScript : MonoBehaviour
{
    Camera cameraToCapture;
    int width = 1920;
    int height = 1080;

    public Texture2D capturedTexture;

    public Sprite CaptureScreen()
    {
        cameraToCapture = Camera.main;

        // RenderTexture 생성
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        cameraToCapture.targetTexture = renderTexture;

        // RenderTexture를 Camera에 설정하고 화면을 렌더링
        RenderTexture.active = renderTexture;
        cameraToCapture.Render();

        // RenderTexture를 Texture2D로 변환
        capturedTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        capturedTexture.Apply();

        Sprite sprite = Texture2DToSprite(capturedTexture);

        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        return sprite;
    }

    public void CleanUpCapturedTexture()
    {
        if (capturedTexture != null)
        {
            Destroy(capturedTexture);
            capturedTexture = null;
            Debug.Log("capturedTexture Destroy");
        }
    }

    public void Start()
    {
        Sprite test = CaptureScreen();
    }

    public void Capture()
    {
        cameraToCapture = Camera.main;

        // 1. RenderTexture 생성
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        cameraToCapture.targetTexture = renderTexture;

        // 2. RenderTexture를 Camera에 설정하고 화면을 렌더링
        RenderTexture.active = renderTexture;
        cameraToCapture.Render();

        // 3. RenderTexture를 Texture2D로 변환
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        // 4. 이미지 파일로 저장
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "ScreenShot.png"), bytes);

        // Clean up
        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        Destroy(texture);

        Debug.Log("Screenshot saved to: " + Path.Combine(Application.persistentDataPath, "ScreenShot.png"));
    }

    //Texture2D를 Sprite로 변환
    private Sprite Texture2DToSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, rect, pivot);
    }
}
