using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health=100;
    // Start is called before the first frame update
    void Start()
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
