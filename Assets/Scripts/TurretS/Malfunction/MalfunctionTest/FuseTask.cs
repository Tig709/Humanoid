using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseTask : MonoBehaviour
{
    public GameObject Lid;

    public List<GameObject> nuts;
    public List<GameObject> Removednuts;

    public List<GameObject> fuses;

    Color Broken = Color.red;

    public Malfunction Cannon;

    public GameObject Error;


    private void Start()
    {
            GameObject fuse = fuses[Random.Range(0, fuses.Count)];
            fuse.GetComponent<Image>().color = Broken;

        Cursor.lockState = CursorLockMode.Confined;

    }

    public void FuseClicked(Button button)
    {
        if (button.GetComponent<Image>().color == Broken)
        {
            Reset();
            Cannon.FixedTurret();
        }
        else
        {
            FailedTask();
        }

    }

    public void Removenut(Button button)
    {
        Removednuts.Add(button.gameObject);
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

    private void FailedTask()
    {
        StartCoroutine(WrongFuse());  
    }

    private IEnumerator WrongFuse()
    {
        Error.SetActive(true);
        yield return new WaitForSeconds(3);
        Error.SetActive(false);
        Reset();
    }


    private void Reset()
    {

        nuts.AddRange(Removednuts);
        Removednuts.Clear();

        foreach (var nut in nuts)
        {
            nut.SetActive(true);
        }

        Lid.SetActive(true);

        foreach (GameObject fusee in fuses)
        {
            fusee.GetComponent<Image>().color = Color.white;
        }

        GameObject fuse = fuses[Random.Range(0, fuses.Count)];
        fuse.GetComponent<Image>().color = Broken;

    }

}
