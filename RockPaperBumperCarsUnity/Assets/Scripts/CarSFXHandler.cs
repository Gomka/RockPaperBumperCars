using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFXHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource tiresScreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    [Header("Sound Adjustments")]
    public float speedToVolumeMultiplier = 0.05f;
    public float speedToPitchMultiplier = 0.2f;
    public float driftToVolumeMultiplier = 0.05f;
    public float collisionVolumeMultiplier = 0.005f;

    CarController carController;
    float desiredEnginePitch = 0.5f;

    void Awake()
    {
        carController = GetComponentInParent<CarController>();
    }

    void Update()
    {
        UdateEngineSFX();
        UpdateTireScreechingSFX();
    }

    private void UdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();

        float desiredEngineVolume = velocityMagnitude * speedToVolumeMultiplier;
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * speedToPitchMultiplier;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2.0f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);

    }

    private void UpdateTireScreechingSFX()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 0.3f, Time.deltaTime * 10);
            }
            else
            {
                tiresScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * driftToVolumeMultiplier;
            }
        }
        else tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 0, Time.deltaTime * 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;
        float collisionVolume = relativeVelocity * collisionVolumeMultiplier;

        carHitAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        carHitAudioSource.volume = collisionVolume;

        if (!carHitAudioSource.isPlaying)
            carHitAudioSource.Play();
    }
}
