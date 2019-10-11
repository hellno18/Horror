using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Transform gButton;
    bool is_gButton;
    GameObject player;
    FlashLight flashlight;
    Health health;
    Slider batterySystem;
    Slider healthSystem;
    // Start is called before the first frame update
    void Awake()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        is_gButton = false;
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
        //get component health
        health = player.GetComponent<Health>();
        //get component flashlight
        flashlight = player.GetComponent<FlashLight>();
        //get component slider
        batterySystem = this.transform.Find("Battery System").GetComponent<Slider>();
        //get component slider 
        healthSystem = this.transform.Find("Health System/HealthSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        batterySystem.value = flashlight.GetBatLight/100;
        healthSystem.value = health.GetHealth / 100;
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
