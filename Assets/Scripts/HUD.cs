using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    private Transform gButton;
    private bool is_gButton;
    private GameObject player;
    private FlashLight flashlight;
    private PlayerController playerController;
    private Slider batterySystem;
    private Slider healthSystem;
    private TextMeshProUGUI stressLVText;
    // Start is called before the first frame update
    void Awake()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        is_gButton = false;
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
        //get component health
        playerController = player.GetComponent<PlayerController>();
        //get component flashlight
        flashlight = player.GetComponentInChildren<FlashLight>();
        //get component slider
        batterySystem = this.transform.Find("Battery System").GetComponent<Slider>();
        //get component slider 
        healthSystem = this.transform.Find("Health System/HealthSlider")
            .GetComponent<Slider>();
        //get component stressLV
        stressLVText = GameObject
            .FindGameObjectWithTag("StressLV")
            .GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        batterySystem.value = flashlight.GetBatLight/100;
        healthSystem.value = playerController.GetHealth / 100;
        stressLVText.text = playerController.StressLV.ToString();
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
