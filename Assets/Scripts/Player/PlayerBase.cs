using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int stressLV=80;
    protected int keyNCount; // reference to key normal
    protected int puzzleCount; // referencet to puzzle count
    protected float health = 100;
    
   

    protected bool keyExit;  //reference to key exit
    protected bool isShake; //reference to camera shaking


    //Global variable
    protected FlashLight flashlight;
    protected HUD hud;
    protected Camera mainCamera;

    private void OnEnable()
    {
        isShake = false;
        keyNCount = 0;
        puzzleCount = 0;
        //Get Component
        flashlight = this.GetComponentInChildren<FlashLight>();
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        mainCamera = this.GetComponentInChildren<Camera>();


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


}
