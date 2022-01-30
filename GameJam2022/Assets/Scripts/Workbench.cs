using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public enum Inventions { None, Radio, WavelengthDevice, Teleporter };

    public Inventions currentInvention = Inventions.Radio;

    int inventionsCompleted = 0;


    public Material redTransparent;
    public Material greenTransparent;
    Renderer renderer;


    public Transform toolBoxSlot;
    public Transform wireSlot;

    bool toolBoxCollected = false;
    bool wireCollected = false;
    bool radioComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        

        switch (currentInvention)
        {       
            case Inventions.None:
                
                break;
            case Inventions.Radio:
                if (other.gameObject.name == "ToolBox")
                {
                    other.gameObject.transform.position = toolBoxSlot.position;
                    other.gameObject.transform.rotation = toolBoxSlot.rotation;
                    toolBoxCollected = true;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    print(other.gameObject.name);
                }
                if (other.gameObject.name == "Wire")
                {
                    other.gameObject.transform.position = wireSlot.position;
                    other.gameObject.transform.rotation = wireSlot.rotation;
                    wireCollected = true;
                    print(other.gameObject.name);
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }

                if (toolBoxCollected && wireCollected)
                {
                    radioComplete = true;
                    renderer.material = greenTransparent;
                }

                break;
            case Inventions.WavelengthDevice:
              
                break;
            case Inventions.Teleporter:

                break;
            default:
                print("Invalid invetion");
                break;

        }
    }
}
