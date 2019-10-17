using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int stressLV;
    protected int health = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Run();
    }
    protected virtual void Run()
    {

    }


 }
