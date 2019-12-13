using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPanel : MonoBehaviour
{
    public enum DoorType
    {
        generator,
        elevator
    }
    [SerializeField] DoorType doorType;
    bool isDoorOn;
    bool isElevatorOn;
    bool isInteract;
    HUD hud;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //reference to door type
        switch (doorType)
        {
            case DoorType.elevator:
                // turn on the electro panel and then elevator can be open
                isElevatorOn = false;
                break;
            case DoorType.generator:
                // turn on the electro panel and then door can be open
                isDoorOn = false;
                break;
        }
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                //Play SE
                audioManager.PlaySE("EMotorDoor");
                switch (doorType)
                {
                    case DoorType.elevator:
                        isElevatorOn = true;
                        break;
                    case DoorType.generator:
                        // turn on the electro panel and then door can be open
                        isDoorOn = true;
                        isInteract = false;
                        break;
                }
            }
        }
       
    }

    /*=============================
    * While collision trigger enter
    ==============================*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDoorOn)
        {
            isInteract = true;
            StartCoroutine(hud.CButtonCoroutine());
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDoorOn)
        {
            isInteract = false;
        }
    }

    /*===========================
     Getter Elevator
     ===========================*/
     public bool GetIsElevator
    {
        get
        {
            return isElevatorOn;
        }
    }

    /*===========================
     Getter Door
     ===========================*/
    public bool GetIsDoor
    {
        get
        {
            return isDoorOn;
        }
    }
}
