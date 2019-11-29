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
        //show the tutorial HUD
        hud.TutorialFLCoroutine();
       
    }
    private void OnTriggerExit(Collider other)
    {
        //Hide
        hud.TutorialFLCoroutine();
        Destroy(this.gameObject);
    }
}
