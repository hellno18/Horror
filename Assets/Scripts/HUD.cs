using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Transform gButton;
    bool is_gButton;
    FlashLight flashlight;
    Slider batterySystem;
    // Start is called before the first frame update
    void Start()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        is_gButton = false;
        //get component flashlight
        flashlight = GameObject.FindGameObjectWithTag("Player").GetComponent<FlashLight>();
        //get component slider
        batterySystem = this.transform.Find("Battery System").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        batterySystem.value = flashlight.GetBatLight/100;
    }

    //toggle G Button Display
    public void GButtonDisplay()
    {
        is_gButton = !is_gButton;
        if (is_gButton)
        {
            gButton.gameObject.SetActive(true);
        }
        else
        {
            gButton.gameObject.SetActive(false);
        }
    }
}
