using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyBase : MonoBehaviour, IDamage
{
    protected float enemyHealth = 100f;
    protected float enemyHealthMax;
    protected float enemySpeed= 6.0f;
    protected int attack;
    protected NavMeshAgent navMesh;
    protected Animator animator;
    protected Slider enemyBar;
    protected float damage=25;
    protected Material mat;

    private void OnEnable()
    {
        enemyHealthMax = enemyHealth;
        //get component animator
        animator = this.GetComponent<Animator>();
        //get component navmeshagent
        navMesh = this.GetComponent<NavMeshAgent>();
        //get component material for shader
        mat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Run();   
    }


    protected virtual void Run()
    {

    }

    /*============================
    * DamageHit to enemy
    ============================*/
    public void AddDamageEnemy(float damage)
    {
        enemyHealth -= damage;
    }

}
