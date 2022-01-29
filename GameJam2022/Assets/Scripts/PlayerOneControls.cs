using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControls : MonoBehaviour
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
    float moveForce = 60;

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
        float HorizontalMovment = Input.GetAxis("Horizontal");
        float verticalMovment = Input.GetAxis("Vertical");

        Vector3 playerMovment = (new Vector3(HorizontalMovment, 0.0f, verticalMovment) * movementSpeed * Time.deltaTime);

        transform.Translate(playerMovment, Space.Self);
    }

    void PickUpCheck()
    {
        if (!carryingObject)
        {
            objectDetected = false;
            objectCarried = null;

            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pickUpRange))
            {
                collision = hit.point;

                if (hit.transform.gameObject.CompareTag("PickUp"))
                {
                    objectCarried = hit.transform.gameObject;
                    objectDetected = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        PickUpObject();
                    }
                }
            }
        }
        else
        {      
            if(objectCarriedBody != null)
            {
                MoveObject();
            }


            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DropObject();
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

    void PickUpObject()
    {

        objectCarriedBody = objectCarried.GetComponent<Rigidbody>();
        objectCarriedBody.useGravity = false;
        objectCarriedBody.drag = 10;
        objectCarriedBody.transform.parent = carriedObjectTarget.transform;

        objectCarried.GetComponent<BoxCollider>().enabled = false;


        carryingObject = true;
    }

    void DropObject()
    {
        objectCarriedBody.drag = 0;
        objectCarriedBody.useGravity = true;
        objectCarried.GetComponent<BoxCollider>().enabled = true;
        objectCarriedBody.transform.parent = null;
        objectCarriedBody = null;
        objectCarried = null;
        carryingObject = false;
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