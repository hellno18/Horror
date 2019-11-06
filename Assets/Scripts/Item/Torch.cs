using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    FlashLight flashlight;
    HUD hud;
    Examines examines;
    bool isInteract;
    // Start is called before the first frame update
    void Start()
    {
        //get component flashlight
        flashlight = GameObject.FindGameObjectWithTag("Player")
            .GetComponentInChildren<FlashLight>();
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component examine
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
        isInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get input "GRAB"
        if (Input.GetButton("Grab") && isInteract)
        {
            if (examines.GetExamineMode)
            {
                examines.UnPause();
                examines.SetExamineMode(false);
            }

            //set count battery
            flashlight.SetIsBringTorch = true;
            //display hud
            hud.GButtonDisplay();
            //destroy
            Destroy(this.gameObject);
        }
    }

    /*=============================
    * While collision trigger enter
    ==============================*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact true
            examines.SetIsInteract(true);
            isInteract = true;
        }
    }


    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact false
            examines.SetIsInteract(false);
            isInteract = false;
        }
    }
}
