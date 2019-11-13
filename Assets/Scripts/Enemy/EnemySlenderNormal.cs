using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySlenderNormal : EnemyBase
{
    Transform player;
    private float timeDis=0;
    private int destPoint = 0;
    FlashLight flashlight;
    float MaxDist = 10f;
    float MinDist = 2.0f;
    bool isChase;
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
        isChase = false;
        GotoNextPoint();
    }

    protected override void Run()
    {  
        enemyBar.value = enemyHealth/enemyHealthMax;
        if (enemyHealth < 0)
        {
            //Hide EnemyBar
            enemyBar.gameObject.SetActive(false);
            StartCoroutine("DissolveEnemyCoroutine");
        }

        Vector3 direction = player.position - this.transform.position;
       //print(Vector3.Distance(player.position, this.transform.position));
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) > 3 && Vector3.Distance(player.position, this.transform.position) < 14 && angle < 180)
        {
            isChase = true;
            //Play anim chase
            animator.SetBool("Chase", true);
            this.transform.position += this.transform.forward * enemySpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f * Time.deltaTime * 50.0f);

            if (flashlight.GetIsLight)
            {
                enemySpeed = 1f;
            }
            else
            {
                enemySpeed = 2f;
                if (direction.magnitude < 2f)
                {
                    //Give Damage to player
                    //
                    //
                    //destroy character
                   // Destroy(gameObject);
                   //TODO spawn UI Dead
                }
                
                
            }
            print(direction.magnitude);
            if (Vector3.Distance(player.position, this.transform.position)>= MinDist)
            {
                //chashing true
                if(isChase) transform.position += transform.forward * enemySpeed * Time.deltaTime;
            }
            if (Vector3.Distance(player.position, this.transform.position) < 3)
            {
                isChase = false;
                animator.SetBool("Chase", false);
                player.GetComponent<PlayerController>().SetHealth =
                    player.GetComponent<PlayerController>().GetHealth - damage;
            }
          
        }
       
        else if (navMesh.remainingDistance < 0.5f)
        {
            //idle and find another to patrol
            GotoNextPoint();
            
        }
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
