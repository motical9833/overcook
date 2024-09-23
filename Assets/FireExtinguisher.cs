using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : GrabAbleObjScript
{
    public ParticleSystem powderParticle;

    bool isSpaying;

    public AudioSource sprayStartSource;
    public AudioSource sprayLoopSource;
    public AudioSource sprayEndSource;
    
    bool wasKeyPressed;

    private void Awake()
    {
        powderParticle.Stop();
    }


    public void SprayPowder(bool _isPressed)
    {
        bool isKeyPressed = Input.GetKey(KeyCode.LeftControl);

        if (isKeyPressed && !wasKeyPressed)
        {
            // 키가 눌리기 시작한 경우 (시작)
            powderParticle.Play();
            isSpaying = true;
            sprayStartSource.Play();
        }
        else if (!isKeyPressed && wasKeyPressed)
        {

            // 키가 떼어진 경우 (종료)
            powderParticle.Stop(withChildren: true, stopBehavior: ParticleSystemStopBehavior.StopEmitting);

            if (sprayLoopSource.isPlaying)
                sprayLoopSource.Stop();
            if (sprayStartSource.isPlaying)
                sprayStartSource.Stop();

            sprayEndSource.Play();
        }
        else if (isKeyPressed && wasKeyPressed)
        {
            if (sprayStartSource.isPlaying)
                return;
            else if (!sprayLoopSource.isPlaying)
                sprayLoopSource.Play();

        }
        // 지속된 경우에는 별도로 처리할 필요 없음 (loopSound가 이미 재생 중임)

        // 현재 프레임의 키 상태를 다음 프레임에서의 이전 상태로 설정
        wasKeyPressed = isKeyPressed;



      
    }

}
