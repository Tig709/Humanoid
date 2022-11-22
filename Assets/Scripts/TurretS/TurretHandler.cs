using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHandler : MonoBehaviour
{
    public GameObject Player;

    public Camera Camera;

    public GridManager gridmanager;


    public GameObject ManualTurret;
    public GameObject AutomaticTurret;

    public GameObject BarrierTurret;
    public GameObject WallPrefab;

    public List<GameObject> TurretList;
    public List<GameObject> BarrierList;
    public List<GameObject> Walls;

    public Canvas TurretSelector;
    public Image Box0, Box1, Box2;
    public Sprite ManualSprite, AutoSprite, BarrierSprite;

    public float turretY = 0;

    public int Spritewheel = 0;


    private int Range = 2;

    private int CurrentTurret;

    private bool fixTurret = false;


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
        Vector3 turretposition = gridmanager.GetNearestPointOnGrid(position);
        turretposition.y = 0;

        GameObject Turret = Instantiate(SelectTurret(type), turretposition, Quaternion.identity);
        Turret.transform.SetParent(this.transform);

        if (type == TurretTypes.Automatic || type == TurretTypes.Manual)
        {
            Turret.transform.LookAt(gridmanager.getClosestPathTile(turretposition).transform);
            TurretList.Add(Turret);
        }
        else
        {
            BarrierList.Add(Turret);
        }
    }

    private void Update()
    {
        updateSelector();
        MakeForcefields();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gridmanager.CheckValidPosition((TurretHandler.TurretTypes)CurrentTurret, Player.transform.position))
            {
                Vector3 placeposition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
                PlaceTurret(placeposition, (TurretHandler.TurretTypes)CurrentTurret);
            }
            else
            {
                if (TurretList.Count != 0)
                {
                    foreach (GameObject turret in TurretList)
                    {
                        if (turret.GetComponent<TurretScript>() != null)
                        {
                            if (turret.GetComponent<TurretScript>().used)
                            {
                                TurretPlayer(true, turret);
                            }
                            else
                            {
                                if (Vector3.Distance(Player.transform.position, turret.transform.position) <= Range)
                                {
                                    if (!turret.GetComponent<TurretScript>().broken)
                                    {
                                        fixTurret = false;

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

                                        fixTurret = true;

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (turret.GetComponent<TurretScript>().broken)
                            {
                                //getmouse = false;
                                TurretSelector.enabled = false;
                                turret.GetComponent<Malfunction>().CurrentTask.SetActive(true);
                                Cursor.lockState = CursorLockMode.Confined;

                                fixTurret = true;

                            }
                        }
                    }
                }
            }
        }

        if (true)
        {
            foreach (var turret in TurretList)
            {
                if (turret.GetComponent<TurretScript>() != null)
                {
                    if (!turret.GetComponent<TurretScript>().broken)
                    {
                        if (!TurretSelector.enabled)
                        {
                            TurretSelector.enabled = true;
                        }
                    }
                }
                else
                {
                        if (!turret.GetComponent<TurretAttackScript>().broken)
                        {
                            if (!TurretSelector.enabled)
                            {
                                TurretSelector.enabled = true;
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
    }

    public void TurretPlayer(bool UsePlayer, GameObject turret)
    {
        Player.GetComponentInChildren<MeshRenderer>().enabled = UsePlayer;
        Camera.enabled = UsePlayer;
        TurretSelector.enabled = UsePlayer;
        turret.GetComponent<TurretScript>().UseTurret(!UsePlayer);

    }

    private void MakeForcefields()
    {
        foreach (var tower1 in BarrierList)
        {
            foreach (var tower2 in BarrierList)
            {
                if (tower1.transform.position == tower2.transform.position)
                {
                    continue;
                }
                else
                {
                    if (tower1.GetComponent<BarrierConnect>().OtherTowerFound == tower2)
                    {
                        if (!tower1.GetComponent<BarrierConnect>().WallMade && !tower2.GetComponent<BarrierConnect>().WallMade)
                        {
                            Vector3 Distance = tower2.transform.position - tower1.transform.position;

                            Vector3 rotation = Vector3.zero;
                            if (Distance.z != 0)
                            {
                                rotation = new Vector3(0, 90, 0);
                            }

                            Distance /= 2;

                            Vector3 Position = tower1.transform.position + Distance;

                            GameObject Wall = Instantiate(WallPrefab, Position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));

                            tower1.GetComponent<BarrierConnect>().WallMade = true;
                            tower2.GetComponent<BarrierConnect>().WallMade = true;

                        }
                    }
                }
            }
        }

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