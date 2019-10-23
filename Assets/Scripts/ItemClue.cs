using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClue : MonoBehaviour
{
    HUD hud;
    Examines examines;
    // Start is called before the first frame update
    void Start()
    {
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component examine
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //While collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hud.GButtonDisplay();
            //interact true
            examines.SetIsInteract(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hud.GButtonDisplay();
            //interact false
            examines.SetIsInteract(false);
        }
    }
}
