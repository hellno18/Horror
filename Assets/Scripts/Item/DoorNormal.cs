﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNormal : MonoBehaviour
{
    bool isInteract;
    bool isOpen;

    // Smoothly open a door
    [SerializeField] private float doorOpenAngle=-80f; //Set either positive or negative number to open the door inwards or outwards
    [SerializeField] private bool islocked;
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float openTime = 0;
    private float timer = 2;
    //Global Variable
    HUD hud;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.x;
        currentRotationAngle = transform.localEulerAngles.x;
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component audiomanager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //reference to timer
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;
        // reference to player controller component
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<IKey>();
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle +
            (isOpen ? doorOpenAngle : 0), openTime), transform.localEulerAngles.y
            , transform.localEulerAngles.z);

        if (Input.GetButtonDown("Interact") && isInteract &&!islocked)
        {
            isOpen = !isOpen;
            if (isOpen) audioManager.PlaySE("open_creaky_door");
            else audioManager.PlaySE("door_creak_closing");
            currentRotationAngle = transform.localEulerAngles.x;
            openTime = 0;
        }

        if (Input.GetButtonDown("Interact") &&islocked && isInteract
            && player.GetKeyNCount()>0 && timer==0)
        {
            islocked = false;
            //remove keycount --
            player.RemoveKeyNCount();
            audioManager.PlaySE("padlock");
        }

        if (Input.GetButtonDown("Interact") && islocked 
            && isInteract && player.GetKeyNCount()>=0 && timer==0)
        {
            timer = 2;
            audioManager.PlaySE("door_knob");
        }


    }

   
    /*=============================
    * While collision trigger enter
    ==============================*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = true;
            StartCoroutine(hud.CButtonCoroutine());
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = false;
        }
    }

    /*=============================
   * SETTER GETTER isOpen
   ==============================*/

    public bool IsOpenFunc
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
        }
    }

}
