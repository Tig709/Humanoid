using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    private Vector3 newMMPos = new Vector3(Screen.width/2,Screen.height/2,0);
    private Vector3 newMMScale = new Vector3(3,3,1);
    private Vector3 startScale = new Vector3(1, 1, 1);
    private Vector3 startPosition = new Vector3();
    private bool canGoBack = false;
    private bool tabReleased = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !canGoBack)
        {
            transform.position = newMMPos;
            transform.localScale = newMMScale;
            canGoBack = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab) && canGoBack) 
        { 
            tabReleased = true; 
        }

        if (Input.GetKeyDown(KeyCode.Tab) && canGoBack && tabReleased) 
        { 
            transform.position = startPosition; 
            transform.localScale = startScale;
            canGoBack = false; 
            tabReleased = false; 
        }
    }
}
