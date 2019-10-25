using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : PlayerBase
{
    private float timer = 5;
    private float currTimer;
    FlashLight flashlight;
    // Start is called before the first frame update
    void Start()
    {
        //set default Health 
        health = 100;
        //set default stress LV
        stressLV = 80;
        flashlight = this.GetComponentInChildren<FlashLight>();
    }

    protected override void Run()
    {
        if (flashlight.GetIsLight)
        {
            currTimer -= Time.deltaTime;
            if (currTimer < 0)
            {
                StressLV -= 1;
                currTimer = timer;
            }

            //while stress lv become 0
            if (StressLV < 80)
            {
                StressLV = 80;
            }
        }
        else
        {
            currTimer -= Time.deltaTime;
            if (StressLV < 90)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    //timer default 5 seconds
                    currTimer = timer;
                }
            }
            else if (StressLV >= 90 && StressLV < 120)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    currTimer = 3;
                }
            }
            else if (StressLV >= 120 && StressLV < 140)
            {
                if (currTimer < 0)
                {
                    StressLV += 1;
                    currTimer = 1;
                }
            }
            else if (StressLV >= 140 && StressLV < 160)
            {
                if (currTimer < 0)
                {
                    StressLV += 2;
                    currTimer = 1;
                }
            }
            else if(stressLV>160)
            {
                StressLV = 160;
                //Dead Show Slenderman face
                //TODO
            }
        }



    }


    //Getter get Health
    public int GetHealth
    {
        get
        {
            return health;
        }
    }

    //Setter Health
    public int SetHealth
    {
        set
        {
            health = value;
        }
    }

    //Setter Getter StressLV
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
