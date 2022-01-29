using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float camRotationSpeed = 1;
    public Transform player;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CamUpdate();
    }

    void CamUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * camRotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * camRotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -7, 15);
        transform.LookAt(this.transform);
        this.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        player.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }
}
