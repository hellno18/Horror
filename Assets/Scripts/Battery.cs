using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    FlashLight flashlight;
    HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        //get component flashlight
        flashlight = GameObject.FindGameObjectWithTag("Player").GetComponent<FlashLight>();
        //get component HUD
        hud = GameObject.Find("Canvas").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input "GRAB"
        if (Input.GetButton("Grab"))
        {
            flashlight.SetBatCount();
            hud.GButtonDisplay();
            Destroy(this.gameObject);
        }
    }

    //While collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
        }
    }
}
