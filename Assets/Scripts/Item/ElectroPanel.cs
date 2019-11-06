using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPanel : MonoBehaviour
{
    bool isDoorOn;
    bool isInteract;
    HUD hud;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        isDoorOn = false;
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                isDoorOn = true;
                audioManager.PlaySE("EMotorDoor");
            }
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
            StartCoroutine(CButtonCoroutine());
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

    IEnumerator CButtonCoroutine()
    {
        hud.CButtonDisplay();
        yield return new WaitForSeconds(1f);
        hud.CButtonDisplay();
    }

    public bool GetElectroPanel
    {
        get
        {
            return isDoorOn;
        }
    }
}
