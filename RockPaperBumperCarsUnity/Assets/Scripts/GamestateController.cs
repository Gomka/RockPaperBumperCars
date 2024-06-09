using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateController : MonoBehaviour
{
    [SerializeField] CarState [] cars;
    int numRock = 0, numPaper = 0, numScissors = 0;

    // Start is called before the first frame update
    void Start()
    {
        BeginRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginRound() {
        numRock = 0; 
        numPaper = 0; 
        numScissors = 0;

        for (int i = 0; i < cars.Length; i += 3)
        {
            cars[i].ForceChangeState(CarState.RockPaperScissorsState.rock);
            cars[i+1].ForceChangeState(CarState.RockPaperScissorsState.paper);
            cars[i+2].ForceChangeState(CarState.RockPaperScissorsState.scissors);

            numRock ++; 
            numPaper ++; 
            numScissors ++;
        }
    }

    public void AddRock() {
        numRock ++;
        numScissors --;
    }

    public void AddPaper() {
        numPaper ++;
        numRock --;
    }

    public void AddScissors() {
        numScissors ++;
        numPaper --;
    }
}
