using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScript : MonoBehaviour
{
    public int UnlockAfterWave = 0;
    public int CurrencyUnlock = 0;

    public Spawner spawner;

    public void Check(int resource)
    {
        if (resource == CurrencyUnlock && UnlockAfterWave == 0)
        {
            Debug.Log("true");
        }
    }

}
