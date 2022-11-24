using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public int ResourceCount;

        public bool InTrigger;

        public Canvas canvas;
        public Image UnlockImage;

        private GameObject trigger;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (InTrigger)
                {
                    trigger.GetComponent<UnlockScript>().Check(ResourceCount);
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Forcefield")
            {
                trigger = other.gameObject;
                //UnlockImage.enabled = true;
                InTrigger = true;
            }
        }
    }
}