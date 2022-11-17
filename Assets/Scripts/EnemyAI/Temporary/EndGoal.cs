using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField]
    private int endGoalLives;

    // Getter EndGoalLives
    public int getEndGoalLives()
    {
        return endGoalLives; 
    }

    // Setter EndGoalLives
    public int setEndGoalLives(int value)
    {
        endGoalLives = value;

        return endGoalLives;
    }

    // Setter for removing one live
    public int setEndGoalLivesMinusOne()
    {
        endGoalLives -= 1;

        Debug.Log(endGoalLives);

        return endGoalLives;
    }

    public void Update()
    {
        //used for testing
       // if (Input.anyKeyDown) { setEndGoalLives(0); }

        //switch to gameover if lives is zero or lower.
        if(endGoalLives <= 0)
        {
            FindObjectOfType<GameManager>().gameOver();
        }
    }
}
