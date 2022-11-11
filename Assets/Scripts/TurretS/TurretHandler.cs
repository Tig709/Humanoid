using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour
{
    public GameObject Player;

    public GameObject ManualTurret;
    public GameObject AutomaticTurret;
    public GameObject BarrierTurret;
    public List<GameObject> TurretList;

    private int Range = 2;

    public enum TurretTypes
    {
        Manual,
        Automatic,
        Barier
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject turret in TurretList)
            {
                if (turret.GetComponent<TurretScript>().used)
                {
                    TurretPlayer(true,turret);
                }
                else
                {
                    if (Vector3.Distance(Player.transform.position, turret.transform.position) <= Range)
                    {
                        if (Player.GetComponentInChildren<Camera>().enabled)
                        {
                            TurretPlayer(false, turret);
                        }
                        else
                        {
                            TurretPlayer(true, turret);
                        }
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 placeposition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
            PlaceTurret(placeposition, TurretHandler.TurretTypes.Manual);
        }
    }

    public void TurretPlayer(bool UsePlayer, GameObject turret)
    {
        Player.GetComponentInChildren<MeshRenderer>().enabled = UsePlayer;
        Player.GetComponentInChildren<Camera>().enabled = UsePlayer;

        turret.GetComponent<TurretScript>().UseTurret(!UsePlayer);

    }
}