using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Cube : MonoBehaviour
{
    private bool isInteract;
    private bool hold;
    private bool isMatch;
    private float timer;
    //Global Variable
    HUD hud;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        isMatch = false;
        isInteract = false;
        timer = 0.1f;
        //Get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
        .GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMatch) {
            hold = false;
            
        } 
        if (Input.GetButtonDown("Interact")&&!isMatch && isInteract)
        {
            hold = true;
           
        }

        if (Input.GetButtonUp("Interact") && !isMatch && isInteract)
        {
            hold = false;
        }

        if (hold)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 1.4f;
                audioManager.PlaySE("slideSFX");
            }
        }

        if (!hold)
        {
            this.transform.parent = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!hold && !isMatch )
        {
            StartCoroutine("CButtonCoroutine");
            isInteract = true;
        }
    }

    IEnumerator CButtonCoroutine()
    {
        hud.CButtonDisplay();
        yield return new WaitForSeconds(1f);
        hud.CButtonDisplay();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzle"))
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPuzzle>();
            //Add count puzzle
            player.AddCountPuzzle();
            isMatch = true;
            hold = false;
            other.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            audioManager.PlaySE("Heavy-Door-Lock--Locking--1-www.fesliyanstudios.com");
            //destroy puzzle
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player") && hold &&!isMatch)
        {
            this.transform.parent = other.transform;
            //Debug.Log("holding");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hold && !isMatch)
        {
            isInteract = false;
        }
        
    }

    public bool GetIsMatch
    {
        get
        {
            return isMatch;
        }
    }
}
