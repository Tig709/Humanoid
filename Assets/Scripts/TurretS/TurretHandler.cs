using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHandler : MonoBehaviour
{
    public GameObject Player;

    public Camera Camera;


    public GameObject ManualTurret;
    public GameObject AutomaticTurret;
    public GameObject BarrierTurret;
    public List<GameObject> TurretList;

    public Canvas TurretSelector;
    public Image Box0, Box1, Box2;
    public Sprite ManualSprite, AutoSprite, BarrierSprite;

    public int Spritewheel = 0;


    private int Range = 2;

    private int CurrentTurret;


    public enum TurretTypes
    {
        Manual,
        Automatic,
        Barier,

    }


    private GameObject SelectTurret(TurretTypes type)
    {
        switch (type)
        {
            case TurretTypes.Manual:
                return ManualTurret;
            case TurretTypes.Automatic:
                return AutomaticTurret;
            case TurretTypes.Barier:
                return BarrierTurret;
        }
        return null;

    }

    public void PlaceTurret(Vector3 position, TurretTypes type)
    {
        GameObject Turret = Instantiate(SelectTurret(type), position, Quaternion.identity);
        Turret.transform.SetParent(this.transform);
        TurretList.Add(Turret);
    }

    private void Update()
    {
        updateSelector();

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject turret in TurretList)
            {
                if (turret.GetComponent<TurretScript>() != null)
                {
                if (turret.GetComponent<TurretScript>().used)
                {
                    TurretPlayer(true,turret);
                }
                else
                {
                    if (Vector3.Distance(Player.transform.position, turret.transform.position) <= Range)
                    {
                        if (!turret.GetComponent<TurretScript>().broken)
                        {
                            if (Camera.enabled)
                            {
                                TurretPlayer(false, turret);
                            }
                            else
                            {

                                TurretPlayer(true, turret);
                            }
                        }
                        else if (turret.GetComponent<TurretScript>().broken)
                        {
                            //getmouse = false;
                            TurretSelector.enabled = false;
                            turret.GetComponent<Malfunction>().CurrentTask.SetActive(true);
                            Cursor.lockState = CursorLockMode.Confined;
                        }
                    }
                }
                }
                else
                {
                    if (Vector3.Distance(Player.transform.position, turret.transform.position) <= Range)
                    {
                        if (turret.GetComponentInChildren<TurretAttackScript>().broken)
                        {
                            //getmouse = false;
                            TurretSelector.enabled = false;
                            turret.GetComponent<Malfunction>().CurrentTask.SetActive(true);
                            Cursor.lockState = CursorLockMode.Confined;
                        }
                    }
                }
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Spritewheel++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Spritewheel--;
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 placeposition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
            PlaceTurret(placeposition, (TurretHandler.TurretTypes)CurrentTurret);
        }
    }

    public void TurretPlayer(bool UsePlayer, GameObject turret)
    {
        Player.GetComponentInChildren<MeshRenderer>().enabled = UsePlayer;
        Camera.enabled = UsePlayer;
        TurretSelector.enabled = UsePlayer;
        turret.GetComponent<TurretScript>().UseTurret(!UsePlayer);

    }


    private void updateSelector()
    {
        switch (Spritewheel)
        {
            case 0:
                CurrentTurret = 0;
                Box0.sprite = ManualSprite;
                Box1.sprite = AutoSprite;
                Box2.sprite = BarrierSprite;
                break;
            case 1:
                CurrentTurret = 1;
                Spritewheel = 1;
                Box0.sprite = AutoSprite;
                Box1.sprite = BarrierSprite;
                Box2.sprite = ManualSprite;
                break;
            case -1:
                CurrentTurret = 2;
                Spritewheel = -1;
                Box0.sprite = BarrierSprite;
                Box1.sprite = ManualSprite;
                Box2.sprite = AutoSprite;
                break;

            case < -1:
                Spritewheel = 1;
                break;
            case > 1:
                Spritewheel = -1;
                break;

        }
    }

}