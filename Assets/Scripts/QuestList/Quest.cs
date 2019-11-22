using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        if (electro.IsClear)
        {
            hud.Toggle1.isOn = true;
        }
    }
}
