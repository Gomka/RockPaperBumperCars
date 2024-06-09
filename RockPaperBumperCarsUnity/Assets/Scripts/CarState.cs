using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarState : MonoBehaviour
{
    
    public enum RockPaperScissorsState {rock, paper, scissors};
    public RockPaperScissorsState rpsState;
    public Sprite rockSprite, paperSprite, scissorsSprite;
    public float invulnerabilityTime = 1.0f;

    bool isChangingState = false;
    SpriteRenderer spriteRenderer;
    GamestateController controller;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        controller = FindObjectOfType<GamestateController>();
    }

    public void ChangeState(RockPaperScissorsState target) {
        if(!isChangingState) {
            StartCoroutine(ChangeStateCoroutine(target));
        }
    }

    public void ForceChangeState(RockPaperScissorsState target) {
        StartCoroutine(ChangeStateCoroutine(target));
    }

    IEnumerator ChangeStateCoroutine(RockPaperScissorsState target) {
        isChangingState = true;

        switch (target)
        {
            case RockPaperScissorsState.rock: spriteRenderer.sprite = rockSprite; this.gameObject.tag = "Rock"; break;
            case RockPaperScissorsState.paper: spriteRenderer.sprite = paperSprite; this.gameObject.tag = "Paper"; break;
            case RockPaperScissorsState.scissors: spriteRenderer.sprite = scissorsSprite; this.gameObject.tag = "Scissors"; break;

            default: break;
        }

        rpsState = target;
        // do animation
        yield return new WaitForSeconds(invulnerabilityTime);

        isChangingState = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent<CarState>(out CarState collisionState)) {

            switch (collisionState.rpsState) {
                case RockPaperScissorsState.rock: 
                    if(this.rpsState == RockPaperScissorsState.scissors) {
                        ChangeState(RockPaperScissorsState.rock);
                        controller.AddRock();
                    }
                    break;
                case RockPaperScissorsState.paper:
                    if(this.rpsState == RockPaperScissorsState.rock) {
                        ChangeState(RockPaperScissorsState.paper);
                        controller.AddPaper();
                    }
                    break;
                case RockPaperScissorsState.scissors:
                    if(this.rpsState == RockPaperScissorsState.paper) {
                        ChangeState(RockPaperScissorsState.scissors);
                        controller.AddScissors();
                    }
                    break;

                default: break;
            }
        }
    }
}
