using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    HUD hud;
    Examines examines;
    //PlayerController player;

    bool isInteract;
    // Start is called before the first frame update
    private void Awake()
    {
        //get component player controller
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component examine
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
        isInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Grab")&& isInteract)
        {
            if (examines.GetExamineMode)
            {
                examines.UnPause();
                examines.SetExamineMode(false);
            }
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<IKey>();
            player.AddKeyCount();
           //display hud
            hud.GButtonDisplay();
            //destroy
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact true
            examines.SetIsInteract(true);
            isInteract = true;
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact false
            examines.SetIsInteract(false);
            isInteract = false;
        }
    }
}
