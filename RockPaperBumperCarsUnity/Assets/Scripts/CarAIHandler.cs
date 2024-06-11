using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIHandler : MonoBehaviour
{

    CarController carController;
    Vector3 targetPosition = Vector3.zero;
    GameObject targetGameObject = null;
    GamestateController gameController;
    CarState currentState;

    void Awake()
    {
        carController = GetComponent<CarController>();
        gameController = FindObjectOfType<GamestateController>();
        currentState = GetComponent<CarState>();
    }


    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        if(AcquireTarget()) {
            inputVector.x = TurnTowardsTarget(true);
            inputVector.y = 1.0f;

            carController.SetInputVector(inputVector);
        }
    }

    bool AcquireTarget() {

        switch (currentState.rpsState) {
            case CarState.RockPaperScissorsState.rock: 
                targetGameObject = GameObject.FindGameObjectWithTag("Scissors");
                break;
            case CarState.RockPaperScissorsState.paper: 
                targetGameObject = GameObject.FindGameObjectWithTag("Rock");
                break;
            case CarState.RockPaperScissorsState.scissors: 
                targetGameObject = GameObject.FindGameObjectWithTag("Paper");
                break;

            default: break;
        }

        if(targetGameObject != null) {
            targetPosition = targetGameObject.transform.position;
            return true;
        }

        return false;
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
}
