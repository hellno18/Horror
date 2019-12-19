using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerBase : MonoBehaviour
{
    protected int stressLV=80;
    protected int keyNCount; // reference to key normal
    protected int puzzleCount; // referencet to puzzle count
    protected float health = 100;


    protected bool isInsideLight; //reference to isInsideLight??
    protected bool keyExit;  //reference to key exit
    protected bool isShake; //reference to camera shaking


    //Global variable
    protected FlashLight flashlight;
    protected HUD hud;
    protected Camera mainCamera;
    protected FirstPersonController controller;
    protected AudioManager audioManager;

    private void OnEnable()
    {
        isShake = false;
        keyNCount = 0;
        puzzleCount = 0;
        //Get Component
        flashlight = this.GetComponentInChildren<FlashLight>();
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        mainCamera = this.GetComponentInChildren<Camera>();
        controller = this.GetComponent<FirstPersonController>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
  .GetComponent<AudioManager>();


        //Playerprefs Result Win / Lose
        if (!PlayerPrefs.HasKey("Result"))
        {
            PlayerPrefs.SetString("Result", "");
        }
    }

    private void Update()
    {
        Run();
        FlashLightRun();
    }
    protected virtual void Run()
    {
        //Overloading
    }
    protected virtual void FlashLightRun()
    {
        //Overloading
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            isInsideLight = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            isInsideLight = false;
        }
    }

}
