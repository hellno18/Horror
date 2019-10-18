using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : PlayerBase
{

    // Start is called before the first frame update
    void Start()
    {
        //set default Health 
        health = 100;
        //set default stress LV
        stressLV = 0;

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

    public int SetHealth
    {
        set
        {
            health = value;
        }
    }

    public int StressLV
    {
        get
        {
            return stressLV;
        }
        set
        {
            stressLV = value;
        }
    }
}
