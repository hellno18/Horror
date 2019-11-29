using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool IsQuestDone { get; set; }
    GameObject keyExit;
    DoorGenerator electro;
    HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        //get component hud
        hud = this.GetComponent<HUD>();
        //get component electropanel
        electro = GameObject.Find("DoorGenerator")
            .GetComponent<DoorGenerator>();
        keyExit = GameObject.Find("key_gold");
        keyExit.SetActive(false);
        IsQuestDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (hud.Toggle1.isOn&& hud.Toggle2.isOn&& hud.Toggle3.isOn)
        {
            IsQuestDone = true;
        }

        if (electro.IsClear)
        {
            hud.Toggle1.isOn = true;
        }

        if (player.GetCountPuzzle() >= 2)
        {
            hud.Toggle2.isOn = true;
            if (keyExit != null)
            {
                keyExit.SetActive(true);
            }
        }

        if (player.GetKeyExit())
        {
            hud.Toggle3.isOn = true;
        }

        
    }
}
