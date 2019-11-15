using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUp : MonoBehaviour
{
    Elevator elevator;
    ElectroPanel electroPanel;
    bool isInteract;

    // Start is called before the first frame update
    void Start()
    {
        elevator = GameObject.Find("PTK_Elevator_3Floors").GetComponent<Elevator>();
        //get component electropanel
        electroPanel = GameObject.Find("ElectoPanel_Elevator/DetectElectro")
            .GetComponent<ElectroPanel>();
        isInteract = false;

    }

    // Update is called once per frame
    void Update()
    {
         //electro true
       if (Input.GetButtonDown("Interact") && isInteract)
       {
            if (!elevator.IsReady)
            {
                StartCoroutine(ElevatorCoroutine());
            }
           
           
        }
       
    }

    IEnumerator ElevatorCoroutine()
    {
        elevator.Move();
        yield return new WaitForSeconds(2f);
        elevator.MoveUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            isInteract = true;          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInteract = false;
        }
    }
}
