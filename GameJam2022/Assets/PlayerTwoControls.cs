using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwoControls : MonoBehaviour
{
    // movement
    float movementSpeed = 6;

    // object pick ups
    float pickUpRange = 6.0f;
    public GameObject objectCarried;
    Rigidbody objectCarriedBody;
    bool objectDetected = false;
    bool carryingObject = false;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    public GameObject carriedObjectTarget;
    public GameObject raycastPoint;
    float moveForce = 60;

    // input
    PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();

        // player 2
        playerInputActions.Player2.Enable();

        playerInputActions.Player2.PickUp.performed += PickUpObject;
        playerInputActions.Player2.Drop.performed += DropObject;
    }


    private void Start()
    {
        objectCarried = null;
        objectCarriedBody = null;
    }

    void Update()
    {
        PlayerMovments();
        PickUpCheck();
    }

    void PlayerMovments()
    {
        Vector2 inputVector = playerInputActions.Player2.Movement.ReadValue<Vector2>();

        float HorizontalMovment = inputVector.x;
        float verticalMovment = inputVector.y;

        Vector3 playerMovment = ((new Vector3(HorizontalMovment, 0.0f, verticalMovment) * movementSpeed) * Time.deltaTime);

        transform.Translate(playerMovment, Space.Self);
    }

    void PickUpCheck()
    {
        if (!carryingObject)
        {
            objectDetected = false;
            objectCarried = null;

            var ray = new Ray(raycastPoint.transform.position, raycastPoint.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pickUpRange))
            {
                collision = hit.point;

                if (hit.transform.gameObject.CompareTag("PickUp"))
                {
                    objectCarried = hit.transform.gameObject;
                    objectDetected = true;                  
                }
            }
        }
        else
        {
            if (objectCarriedBody != null)
            {
                MoveObject();
            }      
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(objectCarried.transform.position, carriedObjectTarget.transform.position) > 0.1f)
        {
            Vector3 moveDirection = (carriedObjectTarget.transform.position - objectCarried.transform.position);
            objectCarriedBody.AddForce(moveDirection * moveForce);
        }
    }

    void PickUpObject(InputAction.CallbackContext context)
    {
        if (objectDetected)
        {
            objectCarriedBody = objectCarried.GetComponent<Rigidbody>();
            objectCarriedBody.useGravity = false;
            objectCarriedBody.drag = 10;
            objectCarriedBody.transform.parent = carriedObjectTarget.transform;

            objectCarried.GetComponent<BoxCollider>().enabled = false;


            carryingObject = true;
        }
    }

    void DropObject(InputAction.CallbackContext context)
    {
        if (carryingObject)
        {
            objectCarriedBody.drag = 0;
            objectCarriedBody.useGravity = true;
            objectCarried.GetComponent<BoxCollider>().enabled = true;
            objectCarriedBody.transform.parent = null;
            objectCarriedBody = null;
            objectCarried = null;
            carryingObject = false;
        }     
    }

    private void OnDrawGizmos()
    {
        if (objectDetected)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
