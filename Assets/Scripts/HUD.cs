using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Transform gButton;
    private bool is_gButton;
    private GameObject player;
    private FlashLight flashlight;
    private PlayerController health;
    private Slider batterySystem;
    private Slider healthSystem;
    // Start is called before the first frame update
    void Awake()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        is_gButton = false;
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
        //get component health
        health = player.GetComponent<PlayerController>();
        //get component flashlight
        flashlight = player.GetComponentInChildren<FlashLight>();
        //get component slider
        batterySystem = this.transform.Find("Battery System").GetComponent<Slider>();
        //get component slider 
        healthSystem = this.transform.Find("Health System/HealthSlider")
            .GetComponent<Slider>();
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
