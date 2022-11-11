using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public Transform Cannon;
    public Transform Aim;
    public GameObject BulletPrefab;

    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    public Canvas canvas;
    public Camera Turretcam;

    public bool used;
    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UseTurret(bool use)
    {
        if (use)
        {
            Turretcam.gameObject.SetActive(true);
            Turretcam.enabled = true;
            used = true;
        }
        else
        {

            Turretcam.enabled = false;
            used = false;
        }
    }

    void Update()
    {
        if (used)
        {
            canvas.gameObject.SetActive(true);
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);
            velocity.x = Mathf.Clamp(velocity.x, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            Cannon.localRotation = Quaternion.AngleAxis(velocity.y, Vector3.forward) * Quaternion.AngleAxis(velocity.x, Vector3.up);


            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(BulletPrefab, Cannon.position, Cannon.rotation);
                var bulletdirection = (Aim.position - Cannon.position).normalized;
                bullet.transform.SetParent(this.transform);
                bullet.GetComponent<Rigidbody>().AddForce(bulletdirection * 300, ForceMode.Force);
                bullet.transform.LookAt(Aim);
            }
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }

    }
}
