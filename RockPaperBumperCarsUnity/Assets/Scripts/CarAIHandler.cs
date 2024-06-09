using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIHandler : MonoBehaviour
{

    CarController carController;
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }


    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        FollowTarget();

        inputVector.x = TurnTowardsTarget(true);
        inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        carController.SetInputVector(inputVector);
    }

    void FollowTarget() {
        if(targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if(targetTransform != null) 
            targetPosition = targetTransform.position;
    }

    float TurnTowardsTarget(bool isChasing) {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);

        if(isChasing) angleToTarget *= -1;

        float steerAmmount = angleToTarget / 45.0f;
        steerAmmount = Mathf.Clamp(steerAmmount, -1.0f, 1.0f);

        return steerAmmount;
    }

    float ApplyThrottleOrBrake(float inputX) {
        //return 1.05f - Mathf.Abs(inputX);
        return 1.0f;
    }
}
