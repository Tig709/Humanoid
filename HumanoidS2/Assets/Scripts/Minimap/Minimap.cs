using UnityEngine;

public class Minimap : MonoBehaviour
{

    //Assign this Camera in the Inspector
    [SerializeField]
    private Camera m_OrthographicCamera;
    private Vector3 newCameraPos = new Vector3(0,10,0);
    private float standardCameraSize = 5.0f;
    private float tabbedInCameraSize = 26.0f;

    [SerializeField]
    private Transform player;
    private bool canGoBack = false;
    private bool tabReleased = false;
    private Vector3 newPosition = new Vector3();

    private void Start()
    {
        this.m_OrthographicCamera = GetComponent<Camera>();

        //This enables the Camera (the one that is orthographic)
        m_OrthographicCamera.enabled = true;
    }

    private void LateUpdate()
    {
        //handle input
        MyInput();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !canGoBack)
        {
            //This enables the orthographic mode
            m_OrthographicCamera.orthographic = true;

            //Set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
            m_OrthographicCamera.orthographicSize = tabbedInCameraSize;

            transform.position = newCameraPos;
            canGoBack = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab) && canGoBack)
        {
            tabReleased = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && canGoBack && tabReleased)
        {
            m_OrthographicCamera.orthographicSize = standardCameraSize;
            MoveCam();
            canGoBack = false;
            tabReleased = false;
        }

        if (!canGoBack && !tabReleased) {
            MoveCam(); 
        }
    }

    void MoveCam()
    {
        newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
