using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{

    ElectroPanel electro;
    bool isInteract;
    // Start is called before the first frame update
    void Start()
    {
        //get component electropanel
        electro = GameObject.Find("ElectoPanel/DetectElectro")
            .GetComponent<ElectroPanel>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (electro.GetElectroPanel)
        {
            if (isInteract)
            {
                //electro true
                if (Input.GetButtonDown("Interact"))
                {
                    //play animation door
                    GameObject.Find("Door1").GetComponent<Animator>()
                        .SetBool("isOpen", true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = false;
        }
    }
}
