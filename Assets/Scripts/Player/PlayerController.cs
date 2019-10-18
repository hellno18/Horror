using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : PlayerBase
{
    private TextMeshProUGUI stressLVText;
    // Start is called before the first frame update
    void Start()
    {
        //set default Health 
        health = 100;
        //set default stress LV
        stressLV = 0;
        stressLVText = GameObject
            .FindGameObjectWithTag("StressLV")
            .GetComponent<TextMeshProUGUI>();
        stressLVText.text = stressLV.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Run()
    {

    }


    //Getter get Health
    public int GetHealth
    {
        get
        {
            return health;
        }
    }
}
