using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    HUD hud;
    private void OnEnable()
    {
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(hud.TutorialFLCoroutine());
        
    }
}
