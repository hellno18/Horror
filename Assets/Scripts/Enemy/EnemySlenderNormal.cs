using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySlenderNormal : EnemyBase
{
    public enum EnemyType
    {
        patrol,
        normal
    }
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float seeSight = 8f;
    [SerializeField] private float distance;
    [SerializeField] private float far = 1.5f;
    [SerializeField] private Transform[] points;
    Transform player;
    private float timeDis=0;
    private int destPoint = 0;
    FlashLight flashlight;
    bool changeRoute;
    bool isDamage;

    private Vector3 randomSpotPoint;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 100;
        //get component player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //get component flashlight
        flashlight = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FlashLight>();
        //get component slender health bar
        enemyBar = this.GetComponentInChildren<Slider>();
        //changeRoute bool
        changeRoute = false;
        navMesh.speed=2.5f;

        GotoNextPoint();
    }

    protected override void Run()
    {
        //Health Bar
        enemyBar.value = enemyHealth / enemyHealthMax;
        if (enemyHealth < 0)
        {
            //Hide EnemyBar
            enemyBar.gameObject.SetActive(false);
            StartCoroutine("DissolveEnemyCoroutine");
        }
        //calculate distance with player and enemy
        distance = Vector3.Distance(player.position, this.transform.position);
        print(distance);
        if (distance < seeSight)
        {
            if (flashlight.GetIsLight)
            {
                navMesh.speed = 1;
            }
            else
            {
                navMesh.speed = 2.5f;
            }
            //CHASE
            enemyState = EnemyState.chase;
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

            //follow player
            navMesh.isStopped = false;
            navMesh.SetDestination(player.position);
            //related between distance and far with player
            if (distance < far)
            {
                //stop nav agent
                navMesh.isStopped = true;
                navMesh.velocity = Vector3.zero;
                            
                //Attack state
                enemyState = EnemyState.attack;
            }
        }
        //Doesn't see anything --> random walk
        else
        {
            enemyState= EnemyState.walk;
        }
        switch (enemyState)
        {
            case EnemyState.chase:
                //chase animation
                animator.SetBool("Chase", true);
                break;
            case EnemyState.attack:
                animator.SetBool("Chase", false);
                animator.SetTrigger("Attack");
                attackSpeed -= Time.deltaTime;
                if (attackSpeed < 0)
                {
                    attackSpeed = 1;
                    //Give Damage to player
                    DamageToPlayer();
                }

                break;
            case EnemyState.walk:
                if (enemyType == EnemyType.normal)
                    if (navMesh.remainingDistance < 0.5f)
                        GotoNextPoint();
                else if(enemyType == EnemyType.patrol)
                {
                   GoToPatrol();
                }
                break;
        }
    }

    private void DamageToPlayer()
    {
        player.GetComponent<PlayerController>().SetHealth =
                            player.GetComponent<PlayerController>().GetHealth - damage;
    }

    /*======================
     * Go to another Point 
     ======================*/
    private void GotoNextPoint()
    {
        float x = Random.Range(0, 30);
        float z = Random.Range(0, -30);
        randomSpotPoint = new Vector3(x, transform.position.y, z);

        // Set the agent to go to the currently selected destination.
        navMesh.destination = randomSpotPoint;
        
    }

    /*======================
    * Go to Patrol Point 
    ======================*/
    void GoToPatrol()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        navMesh.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }



    /*============================
    * Dissolve material coroutine
    ============================*/
    IEnumerator DissolveEnemyCoroutine()
    {
        timeDis += Time.deltaTime;
        mat.SetFloat("_DissolveAmount", timeDis / 2 + 0.1f);
        yield return new WaitForSeconds(0.3f);
        if (mat.GetFloat("_DissolveAmount") >= 1)
        {
            Destroy(gameObject);
        }
    }

    

    /*============================
    * DamageHit to enemy
    ============================*/
    public void DamageHitEnemy(float damage)
    {
        enemyHealth -= damage;
    }

}
