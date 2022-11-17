using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGoalLivesUI : MonoBehaviour
{
    // Get the Endgoal
    EndGoal endGoal;

    private TextMeshProUGUI endGoalLives;

    void Start()
    {

        endGoalLives = this.GetComponent<TextMeshProUGUI>();

        // Find the endGoal GameObject
        endGoal = GameObject.FindObjectOfType<EndGoal>();

        // Set the text
        endGoalLives.text = "Lives: " + endGoal.getEndGoalLives().ToString();
    }

    void Update()
    {
        endGoalLives.text = "Lives: " + endGoal.getEndGoalLives().ToString();
    }
}
