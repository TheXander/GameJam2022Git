using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCam : MonoBehaviour
{
    public float camRotationSpeed = 2f;
    public Transform player;
    float mouseX, mouseY;


    PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();

        // player 1
        playerInputActions.Player1.Enable();
    }

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

        Vector2 inputVector = playerInputActions.Player1.CameraMovement.ReadValue<Vector2>();

        //mouseX += Input.GetAxis("Mouse X") * camRotationSpeed;
        //mouseY -= Input.GetAxis("Mouse Y") * camRotationSpeed;

        mouseX += inputVector.x / camRotationSpeed;
        mouseY -= inputVector.y / camRotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -20, 30);
        transform.LookAt(this.transform);
        this.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        player.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }
}
