using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectmenuScript : MonoBehaviour
{
    [SerializeField]
    int selectedTurretID = 0;
    [SerializeField]
    Vector3[] turretImagePosArray = new Vector3[3];
    [SerializeField]
    bool menuOpen = false;
    [SerializeField]
    Image selectorImage, manualImage, autoImage, barrierImage;
    [SerializeField]
    float positionXOffset = 489.3334f, positionYOffset = 267.6667f;


    private void Start()
    {
        CloseMenu();
        turretImagePosArray[0] = new Vector3(-394 + positionXOffset, 172 + positionYOffset, 0);
        turretImagePosArray[1] = new Vector3(-192 + positionXOffset, 172 + positionYOffset, 0);
        turretImagePosArray[2] = new Vector3(102 + positionXOffset, 181 + positionYOffset, 0);
    }

    private void OpenMenu()
    {
        menuOpen = true;
        selectorImage.enabled = true;
        manualImage.enabled = true;
        autoImage.enabled = true;
        barrierImage.enabled = true;
    }

    private void CloseMenu()
    {
        menuOpen = false;
        selectorImage.enabled = false;
        manualImage.enabled = false;
        autoImage.enabled = false;
        barrierImage.enabled = false;
    }

    private void SetSelectedTurret(int turretID)
    {
        selectedTurretID = turretID;
        selectorImage.transform.position = turretImagePosArray[selectedTurretID];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedTurret(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedTurret(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedTurret(2);
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (menuOpen == false)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
}
