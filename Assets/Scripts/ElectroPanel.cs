using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPanel : MonoBehaviour
{
    bool isDoorOn;
    bool isInteract;

    // Start is called before the first frame update
    void Start()
    {
        isDoorOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                isDoorOn = true;
            }
        }
       
    }

    /*=============================
    * While collision trigger enter
    ==============================*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = true;
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = false;
        }
    }

    public bool GetElectroPanel
    {
        get
        {
            return isDoorOn;
        }
    }
}
