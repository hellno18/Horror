using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyBase : MonoBehaviour
{
    protected float enemyHealth = 100f;
    protected float enemySpeed= 6.0f;
    protected int attack;
    protected NavMeshAgent navMesh;
    protected Animator animator;
    protected Slider enemyBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Run();   
    }


    protected virtual void Run()
    {

    }
}
