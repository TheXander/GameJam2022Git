using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6.0f;

    // Update is called once per frame
    void Update()
    {
    //    float Horizontal = Input.GetAxisRaw("Horizontal");
    //    float Vertical = Input.GetAxisRaw("Vertical");

    //    Vector3 direction = new Vector3(Horizontal, 0.0f, Vertical).normalized;


    //    if(direction.magnitude >= 0.1f)
    //    {
    //        controller.Move(direction * speed * Time.deltaTime);           
    //    }
    }
}
