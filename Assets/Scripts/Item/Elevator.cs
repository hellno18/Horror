using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    
    ElectroPanel electroPanel;
    Transform rightDoor;
    Transform leftDoor;
    bool isInteract;
    bool isOpen;
    private float openTime = 0;
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster
    private float currentLeftX;
    private float defaultLeftX;
    private float currentRightX;
    private float defaultRightX;
    // Start is called before the first frame update
    void Start()
    {
        //get component electropanel
        electroPanel = GameObject.Find("ElectoPanel_Elevator/DetectElectro")
            .GetComponent<ElectroPanel>();
        //reference to interact Hall elevator button 
        isInteract = false;
        //reference to left door
        leftDoor = this.transform.Find("PTK_Elevator/Left").GetComponent<Transform>();
        //reference to right door
        rightDoor = this.transform.Find("PTK_Elevator/Right").GetComponent<Transform>();
        isOpen = false;
        currentLeftX = leftDoor.position.z;
        defaultLeftX = leftDoor.position.z;
        currentRightX = rightDoor.position.z;
        defaultRightX = rightDoor.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //IF interact with hall button
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        leftDoor.transform.position = new Vector3(leftDoor.position.x, leftDoor.transform.position.y, Mathf.Lerp(currentLeftX,
            defaultLeftX + (isOpen ? 1.9f : 0),
            openTime));
        rightDoor.transform.position = new Vector3(rightDoor.position.x, rightDoor.transform.position.y, Mathf.Lerp(currentRightX,
            defaultRightX + (isOpen ? -1.9f : 0),
            openTime));
        if (electroPanel.GetIsElevator)
        {
            //electro true
            if (Input.GetButtonDown("Interact") && isInteract)
            {
                isOpen = !isOpen;
                openTime = 0;
                currentLeftX = leftDoor.position.z;
                currentRightX = rightDoor.position.z;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& electroPanel.GetIsElevator)
        {
            isInteract = true;
        }
        else
        {
            print("No power");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && electroPanel.GetIsElevator)
        {
            isInteract = false;
        }
    }
}
