using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    CarController carController;
    TrailRenderer trailRenderer;

    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    void Update()
    {
        trailRenderer.emitting = carController.IsTireScreeching(out float lateralVelocity, out bool isBraking);
    }
}
