using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int stressLV=80;
    protected float health = 100;

    // Start is called before the first frame update
    void Start()
    {

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
