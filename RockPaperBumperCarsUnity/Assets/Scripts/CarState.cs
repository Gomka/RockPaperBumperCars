using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(RockPaperScissorsState target) {
        if(!isChangingState) {
            StartCoroutine(ChangeStateCoroutine(target));
        }
    }

    IEnumerator ChangeStateCoroutine(RockPaperScissorsState target) {
        isChangingState = true;

        rpsState = target;
        switch (target)
        {
            case RockPaperScissorsState.rock: spriteRenderer.sprite = rockSprite; break;
            case RockPaperScissorsState.paper: spriteRenderer.sprite = paperSprite; break;
            case RockPaperScissorsState.scissors: spriteRenderer.sprite = scissorsSprite; break;

            default: break;
        }
        // do animation

        yield return new WaitForSeconds(.5f);
        isChangingState = false;
    }
}
