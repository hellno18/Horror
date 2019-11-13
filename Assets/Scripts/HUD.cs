using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class HUD : MonoBehaviour
{
    private Transform gButton;
    private Transform cButton;
    private Image staminaSystem;
    private bool is_gButton;
    private bool is_cButton;
    private GameObject player;
    private FlashLight flashlight;
    private PlayerController playerController;
    private Slider batterySystem;
    private Slider healthSystem;
    private TextMeshProUGUI stressLVText;
    private Transform tutorialFlashlight;
    // Start is called before the first frame update
    void Awake()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        //Interact button
        cButton = this.transform.Find("InteractButton[C]");
        is_gButton = false;
        is_cButton = false;
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
        //get component stamina
        staminaSystem = transform.Find("Stamina System").GetComponent<Image>();
        //get component tutorial Flashlight
        tutorialFlashlight = transform.Find("TutorialFlashLight").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        batterySystem.value = flashlight.GetBatLight/100;
        healthSystem.value = playerController.GetHealth / 100;
        stressLVText.text = playerController.StressLV.ToString();
        //Stamina
        var staminaPlayer = player.GetComponent<FirstPersonController>().GetStamina;
        var staminaPlayerDefault = player.GetComponent<FirstPersonController>().GetStaminaDefault;
        staminaSystem.fillAmount = staminaPlayer / staminaPlayerDefault;
        if (staminaPlayer < 5) staminaSystem.transform.gameObject.SetActive(true);
        else staminaSystem.transform.gameObject.SetActive(false);
    }

    /*============================
     * Tutorial flashlight
     ============================*/
    public IEnumerator TutorialFLCoroutine()
    {
        tutorialFlashlight.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        tutorialFlashlight.gameObject.SetActive(false);
        yield break;
    }

    /*=============================
    * toggle G Button Display
    ==============================*/
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

    public void CButtonDisplay()
    {
        is_cButton = !is_cButton;
        if (is_cButton)
        {
            cButton.gameObject.SetActive(true);
        }
        else
        {
            cButton.gameObject.SetActive(false);
        }
    }
}
