using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class ExitDoor : MonoBehaviour
{
    bool isInteract;
    HUD hud;
    FirstPersonController fpsController;
    // Start is called before the first frame update
    void Start()
    {
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        fpsController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact")&& isInteract)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            StartCoroutine(player.ResultCoroutine());
            //TODO unlock mouse
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteract = true;
        StartCoroutine(hud.CButtonCoroutine());
    }
}
