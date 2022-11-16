using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseTask : MonoBehaviour
{
    public GameObject Lid;

    public List<GameObject> nuts;

    public List<GameObject> fuses;

    public List<GameObject> brokeFuse;

    Color Broken = Color.red;

    public Malfunction Cannon;


    private void Start()
    {
            GameObject fuse = fuses[Random.Range(0, fuses.Count)];
            fuse.GetComponent<Image>().color = Broken;

            brokeFuse.Add(fuse);

        Cursor.lockState = CursorLockMode.Confined;

    }

    public void FuseClicked(Button button)
    {
        if (button.GetComponent<Image>().color == Broken)
        {
            Cannon.FixedTurret();
        }

    }

    public void Removenut(Button button)
    {
        nuts.Remove(button.gameObject);
        button.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (nuts.Count == 0)
        {
            Lid.SetActive(false);
        }
    }

}
