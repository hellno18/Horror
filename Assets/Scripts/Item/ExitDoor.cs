using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    bool isInteract;
    HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact")&& isInteract)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            StartCoroutine(player.ResultCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteract = true;
        StartCoroutine(hud.CButtonCoroutine());
    }
}
