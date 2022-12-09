using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour{

    public static CameraController instance;

    float movementTime;
    float totalMovementTime;
    float initialIntensity;

    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin noise;

    void Awake(){
        
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        instance = this;
    }
    void Start(){
    }

    public void Shake(float intensity, float frequency, float time){

        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = frequency;
        initialIntensity = intensity;
        totalMovementTime = time;
        movementTime = time;
    }

    void Update(){
        
        if(movementTime > 0){

            movementTime -= Time.deltaTime;
            noise.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (movementTime / totalMovementTime));
        }
    }
}