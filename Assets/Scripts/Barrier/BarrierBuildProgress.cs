using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBuildProgress : MonoBehaviour
{
    [SerializeField]
    float barTimer = 0f, barTimerMax;


    [SerializeField]
    Image radialProgressBar;

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            barTimer += Time.deltaTime / barTimerMax;
        }
        else
        {
            barTimer = 0;
        }
        radialProgressBar.fillAmount = barTimer;
    }
}
