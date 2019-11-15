using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    
    ElectroPanel electroPanel;
    Transform rightDoor;
    Transform leftDoor;
    Transform boxElevator;
    bool isInteract;
    bool isOpen;
    bool isReady;
    private float openTime = 0;
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster
    private float currentLeftX;
    private float defaultLeftX;
    private float currentRightX;
    private float defaultRightX;
    private float ptk_Elevator_Y;
    private float ptk_Elevator_Default_Y;
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
        //reference to box elevator
        boxElevator = this.transform.Find("PTK_Elevator").GetComponent<Transform>();
        isOpen = false;
        currentLeftX = leftDoor.position.z;
        defaultLeftX = leftDoor.position.z;
        currentRightX = rightDoor.position.z;
        defaultRightX = rightDoor.position.z;
        ptk_Elevator_Y = boxElevator.position.y;
        ptk_Elevator_Default_Y = boxElevator.position.y;

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
        //MoveUp;
        boxElevator.position = new Vector3(boxElevator.position.x,
           Mathf.Lerp(ptk_Elevator_Y, ptk_Elevator_Default_Y + (isReady ? 4 : 0), openTime), boxElevator.position.z);

        if (electroPanel.GetIsElevator)
        {
            //electro true
            if (Input.GetButtonDown("Interact") && isInteract)
            {
                Move();
            }
        }
    }


    public void Move()
    {
        isOpen = !isOpen;
        openTime = 0;
        currentLeftX = leftDoor.position.z;
        currentRightX = rightDoor.position.z;
    }

    public void MoveUp()
    {
        isReady = !isReady;
        openTime = 0;
        ptk_Elevator_Y = boxElevator.position.y;
        if(isReady)
        isOpen = !isOpen;
    }

    public bool IsReady
    {
        get
        {
           return isReady;
        }
        set
        {
            isReady = value;
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
