using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationFactor = 10.0f, turnRate = 3.0f, driftFactor = 0.9f, maxSpeed = 10f;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D carRigidBody;

    void Awake()
    {
        carRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.8f && accelerationInput < 0)
            return;

        if (accelerationInput == 0)
        {
            carRigidBody.drag = Mathf.Lerp(carRigidBody.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else carRigidBody.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        float minSpeedToTurn = (carRigidBody.velocity.magnitude / 8);
        minSpeedToTurn = Mathf.Clamp01(minSpeedToTurn);

        rotationAngle -= steeringInput * turnRate * minSpeedToTurn;

        carRigidBody.MoveRotation(rotationAngle);
    }
    
    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody.velocity, transform.right);

        carRigidBody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
