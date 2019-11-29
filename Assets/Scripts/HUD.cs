using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class HUD : MonoBehaviour
{
    //Toggle Quest
    public Toggle Toggle1 { get; set; }
    public Toggle Toggle2 { get; set; }
    public Toggle Toggle3 { get; set; }

    private Transform gButton;
    private Transform cButton;
    private Image staminaSystem;
    private bool is_gButton;
    private bool is_cButton;
    private bool is_questButton;
    private bool isOnTutorial;
    private GameObject player;
    private FlashLight flashlight;
    private PlayerController playerController;
    private Slider batterySystem;
    private Slider healthSystem;
    private TextMeshProUGUI stressLVText;
    private Transform tutorialFlashlight;
    private Transform questObjective;

    // Start is called before the first frame update
    void Awake()
    {
        //Grab button
        gButton = this.transform.Find("GrabButton[G]");
        //Interact button
        cButton = this.transform.Find("InteractButton[C]");
        is_gButton = false;
        is_cButton = false;
        is_questButton = false;
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
        staminaSystem = this.transform.Find("Stamina System").GetComponent<Image>();
        //get component tutorial Flashlight
        tutorialFlashlight = this.transform.Find("TutorialFlashLight").GetComponent<Transform>();
        //get component  quest objective
        questObjective = this.transform.Find("QuestObjective").GetComponent<Transform>();

        //Toggle
        Toggle1 = this.transform.Find("QuestObjective/Toggle1").GetComponent<Toggle>();
        Toggle2 = this.transform.Find("QuestObjective/Toggle2").GetComponent<Toggle>();
        Toggle3 = this.transform.Find("QuestObjective/Toggle3").GetComponent<Toggle>();

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

    public IEnumerator CButtonCoroutine()
    {
        CButtonDisplay();
        yield return new WaitForSeconds(1f);
        CButtonDisplay();
    }

    /*============================
     * Tutorial flashlight
     ============================*/
    public void TutorialFLCoroutine()
    {
        isOnTutorial = !isOnTutorial;
        if (isOnTutorial)
        {
            tutorialFlashlight.gameObject.SetActive(true);
        }
        else
        {
            tutorialFlashlight.gameObject.SetActive(false);
        }
       
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
    /*=============================
    * toggle C Button Display
    ==============================*/
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

    /*=============================
    * toggle Quest Objective Button Display
    ==============================*/
    public void QuestDisplay()
    {
        is_questButton = !is_questButton;
        if (is_questButton)
        {
            questObjective.gameObject.SetActive(true);
        }
        else
        {
            questObjective.gameObject.SetActive(false);
        }
    }
}
