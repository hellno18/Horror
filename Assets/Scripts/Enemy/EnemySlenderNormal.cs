using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySlenderNormal : EnemyBase
{
    public enum EnemyType
    {
        boss,
        normal
    }
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float seeSight = 8f;
    [SerializeField] private float distance;
    [SerializeField] private float far = 0.5f;
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float xMax=45f;
    [SerializeField] private float zMin = 0f;
    [SerializeField] private float zMax = -30f;

    private float startTimeShot=2f;
    private float timeShot;
    //Prefab Gameobject Brain
    private GameObject brainPrefap;
    private Transform player;
    private float timeDis=0;
    private int destPoint = 0;
    private bool isDamage;
    private bool isInsideLight;
    private bool isAroundPlayer;
    private float currTimer;
    private Light enemyLight;
    private Vector3 randomSpotPoint;

    //Global Variable
    FlashLight flashlight;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //get component player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //get component flashlight
        flashlight = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FlashLight>();
        //get component slender health bar
        enemyBar = this.GetComponentInChildren<Slider>();
        enemyLight = this.transform.Find("Root/Base/Back/Back_2/Neck/Point Light").GetComponent<Light>();
        //get component AudioManager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();


        //INIT
        enemyHealth = 100;
        currTimer = 1f;
        // default turn off light
        enemyLight.gameObject.SetActive(false);

        navMesh.speed = 2.5f;
        timeShot = startTimeShot;
        //=======================================================================

        switch (enemyType)
        {
            case EnemyType.normal:
                //default go to next point
                GotoNextPoint();
                break;
            case EnemyType.boss:
                brainPrefap = Resources.Load<GameObject>("Prefaps/brainAttack");
                enemyHealth = 300f;
                break;
        }
       
    }

    protected override void Run()
    {
        //Health Bar
        enemyBar.value = enemyHealth / enemyHealthMax;
        if (enemyHealth < 0)
        {
            //Hide EnemyBar
            enemyBar.gameObject.SetActive(false);
            //light enemy
            enemyLight.gameObject.SetActive(false);
            StartCoroutine("DissolveEnemyCoroutine");
        }
        //calculate distance with player and enemy
        distance = Vector3.Distance(player.position, this.transform.position);

        // when distance smaller rather than seesight, enemy chase player
        if (distance < seeSight)
        {
            if (flashlight.GetIsLight||isInsideLight)
            {
                this.enemyLight.gameObject.SetActive(false);
                navMesh.speed = 1;
            }
            else if(!flashlight.GetIsLight||!isInsideLight)
            {
                this.enemyLight.gameObject.SetActive(true);
                navMesh.speed = 2.5f;
            }
            //CHASE
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            if (enemyType == EnemyType.normal)
            {
                enemyState = EnemyState.chase;
                

                //follow player
                navMesh.isStopped = false;
                navMesh.SetDestination(player.position);
            }
            
            if(enemyType == EnemyType.boss && enemyState == EnemyState.chase)
            {
                //follow player
                navMesh.isStopped = false;
                navMesh.SetDestination(player.position);
            }
            



          

            //related between distance and far with player (NORMAL)
            if (distance < far && enemyType == EnemyType.normal)
            {
                //stop nav agent
                navMesh.isStopped = true;
                navMesh.velocity = Vector3.zero;
                            
                //Attack state
                enemyState = EnemyState.attack;
            }

            //(BOSS)
            if (distance > far && enemyType == EnemyType.boss)
            {
                //Attack state
                enemyState = EnemyState.fire;
              
            }
            else if(distance>2 && distance<3)
            {
                //Attack state
                enemyState = EnemyState.chase;
            }
            else if(distance<1.5f)
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

        if (enemyType == EnemyType.normal)
        {
            switch (enemyState)
            {
                case EnemyState.chase:
                    //chase animation
                    animator.SetBool("Chase", true);
                    //Enemy is around player
                    isAroundPlayer = true;
                    break;
                case EnemyState.attack:
                    animator.SetBool("Chase", false);
                    animator.SetTrigger("Attack");
                    attackSpeed -= Time.deltaTime;
                    break;
                case EnemyState.walk:
                    if (navMesh.remainingDistance < 0.5f)
                    {
                        //Enemy is around player
                        isAroundPlayer = false;
                        animator.SetBool("Chase", false);
                        GotoNextPoint();
                    }
                    break;
            }
        }
        if(enemyType == EnemyType.boss)
        {
            switch (enemyState)
            {
                case EnemyState.chase:
                    //chase animation
                    animator.SetBool("Chase", true);
                    //Enemy is around player
                    isAroundPlayer = true;
                    break;
                case EnemyState.fire:
                    //chase animation
                    animator.SetBool("Chase", true);
                    StartCoroutine(AttackFire());
                    //animator.SetTrigger("Attack");
                    attackSpeed -= Time.deltaTime;
                    break;
                case EnemyState.attack:
                    animator.SetBool("Chase", true);
                    animator.SetTrigger("Attack");
                    attackSpeed -= Time.deltaTime;
                    break;
            }
        }

        // AroundPlayer
        if (isAroundPlayer)
        {
            currTimer -= Time.deltaTime;
            if (currTimer < 0&& player.GetComponent<PlayerController>().StressLV<140)
            {
                player.GetComponent<PlayerController>().StressLV += 1;
                currTimer = 1f;
            }
            else if(currTimer < 0 && player.GetComponent<PlayerController>().StressLV > 140)
            {
                player.GetComponent<PlayerController>().StressLV += 1;
                currTimer = 0.5f;
            }
           
        }
        
    }

    private IEnumerator AttackFire()
    {
        if (timeShot <= 0)
        {
            GameObject objBall = Instantiate(brainPrefap,new Vector3(this.transform.position.x, this.transform.position.y+1.5f, this.transform.position.z), Quaternion.identity) as GameObject;
            Rigidbody rb = objBall.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10;
            timeShot = startTimeShot;
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
        
        yield return new WaitForSeconds(2f);
        animator.SetBool("Chase", false);
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
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);
        randomSpotPoint = new Vector3(x, transform.position.y, z);

        // Set the agent to go to the currently selected destination.
        navMesh.destination = randomSpotPoint;
        
    }

    


    /*============================
    * Dissolve material coroutine
    ============================*/
    IEnumerator DissolveEnemyCoroutine()
    {
        if (timeDis < 0.01f)
        {
            //sfx dead slenderman
            audioManager.PlaySE("blood_guts_spill");
        }
        timeDis += Time.deltaTime;
        mat.SetFloat("_DissolveAmount", timeDis / 2 + 0.1f);
        
        yield return new WaitForSeconds(0.3f);
        if (mat.GetFloat("_DissolveAmount") >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackSpeed < 0)
            {
                attackSpeed = 1;
                //Give Damage to player
                DamageToPlayer();
                other.GetComponent<PlayerController>().CameraShake();
                audioManager.PlaySE("blood_guts_spill");
            }
        }
        if (other.gameObject.CompareTag("Light"))
        {
            isInsideLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            isInsideLight = false;
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
