using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySlenderPatrol : EnemyBase
{
    [SerializeField] Transform[] points;
    Transform player;
    private float timeDis=0;
    private int destPoint = 0;
    FlashLight flashlight;
    bool isDamage;


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
        //Find another point
        GotoNextPoint();
    }

    protected override void Run()
    {  
        enemyBar.value = enemyHealth/ enemyHealthMax;
        if (enemyHealth < 0)
        {
            //Hide EnemyBar
            enemyBar.gameObject.SetActive(false);
            StartCoroutine("DissolveEnemyCoroutine");
        }

        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 30 && angle < 180)
        {
            //Play anim chase
            animator.SetBool("Chase", true);

            if (flashlight.GetIsLight)
            {
                enemySpeed = 1.5f;
            }
            else
            {
                enemySpeed = 6f;
                if (direction.magnitude < 3.5f)
                {
                    //Give Damage to player
                    player.GetComponent<PlayerController>().SetHealth = 
                        player.GetComponent<PlayerController>().GetHealth-damage;
                    animator.SetBool("Chase", false);
                    //destroy character
                    Destroy(gameObject);
                   //TODO spawn UI Dead
                }
                
            }
            this.transform.position += this.transform.forward * enemySpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f * Time.deltaTime * 50.0f);

            if (direction.magnitude > 5)
            {
                //chashing true
                this.transform.Translate(0, 0, 0.05f);
            }

        }
       
        else if (navMesh.remainingDistance < 0.5f)
        {
            //idle and find another to patrol
            GotoNextPoint();
            //animator.SetBool("Chase", false);
        }
    }

    /*======================
    * Go to another Point 
    ======================*/
    void GotoNextPoint()
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


}
