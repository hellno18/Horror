using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    //Global Variable
    HUD hud;
    private bool hold;
    private bool isMatch;
    
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        isMatch = false;
        //Get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMatch) {
            hold = false;
        } 
        if (Input.GetButtonDown("Interact"))
        {
            hold = true;
        }
        if (Input.GetButtonUp("Interact"))
        {
            hold = false;
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
            //print(player.GetCountPuzzle());
            isMatch = true;
            other.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            //destroy puzzle
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player") && hold &&!isMatch)
        {
            this.transform.parent = other.transform;
            //Debug.Log("holding");
        }
    }
}
