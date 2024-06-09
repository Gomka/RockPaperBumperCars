using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarState : MonoBehaviour
{
    
    public enum RockPaperScissorsState {rock, paper, scissors};
    public RockPaperScissorsState rpsState;
    public Sprite rockSprite, paperSprite, scissorsSprite;

    bool isChangingState = false;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            case RockPaperScissorsState.rock: spriteRenderer.sprite = rockSprite; break;
            case RockPaperScissorsState.paper: spriteRenderer.sprite = paperSprite; break;
            case RockPaperScissorsState.scissors: spriteRenderer.sprite = scissorsSprite; break;

            default: break;
        }

        // do animation

        yield return new WaitForSeconds(0.5f);
        rpsState = target;
        isChangingState = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent<CarState>(out CarState collisionState)) {
            
            switch (collisionState.rpsState) {
                case RockPaperScissorsState.rock: 
                    if(this.rpsState == RockPaperScissorsState.scissors) ChangeState(RockPaperScissorsState.rock);
                    break;
                case RockPaperScissorsState.paper:
                    if(this.rpsState == RockPaperScissorsState.rock) ChangeState(RockPaperScissorsState.paper);
                    break;
                case RockPaperScissorsState.scissors:
                    if(this.rpsState == RockPaperScissorsState.paper) ChangeState(RockPaperScissorsState.scissors);
                    break;

                default: break;
            }
        }
    }
}
