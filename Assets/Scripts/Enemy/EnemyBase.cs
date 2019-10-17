using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected int enemyHealth = 20;
    protected float enemySpeed= 6.0f;
    protected int attack;
    protected NavMeshAgent navMesh;
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = this.transform.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeVelocity();   
    }


    protected virtual void ComputeVelocity()
    {

    }
}
