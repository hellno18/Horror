using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int stressLV=80;
    protected float health = 100;
    protected int keyCount;
    protected int puzzle;

    private void OnEnable()
    {
        keyCount = 0;
        puzzle = 0;
    }

    private void Update()
    {
        Run();
        FlashLightRun();
    }
    protected virtual void Run()
    {
        //Overloading
    }
    protected virtual void FlashLightRun()
    {
        //Overloading
    }


}
