using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : PlayerBase
{
    private bool isLight;
    private float batLevel=50f;
    private int batCount=0;
    private float distance = 15f;
    private float damage=0.5f;
    private Transform hand;
    private Camera mainCamera;
    private EnemySlender enemySlender;
    private Examines examines;
    private float timer=5;
    private float currTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        currTimer = timer;
        //light off
        isLight = false;
        //find hand component
        hand = this.transform.Find("Hand");
        mainCamera = this.transform.GetComponent<Camera>();
        //get component examines
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
    }

    //OverLoading flashlight
    protected override void FlashLightRun()
    {
        //if turn on light will decrease
        if (isLight)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player")
             .GetComponent<PlayerController>();
            //current time minus
            currTimer -= Time.deltaTime;
            // timer become 0
            if (currTimer < 0)
            {
                player.StressLV -= 1;
                currTimer = timer;
            }

            //while stress lv become 0
            if (player.StressLV < 0)
            {
                player.StressLV -= 0;
            }

            //decrease light power
            batLevel -= Time.deltaTime;

            //Raycast to Gameobject
            RaycastHit hit;
            if (Physics.Raycast(this.mainCamera.transform.position,
                this.mainCamera.transform.forward, out hit, distance))
            {
                //Give damage to enemy with flashlight
                if (hit.collider.tag == "Slender")
                {
                    enemySlender = hit.collider.transform.GetComponent<EnemySlender>();
                    enemySlender.DamageHitEnemy(damage);
                    enemySlender.GetComponent<Animator>().SetTrigger("Hit");
                }
            }

            Light light = GameObject.Find("Spotlight")
                .GetComponent<Light>();
            Light pointLight = GameObject.Find("PointLight")
                .GetComponent<Light>();
            //turn off the light while power battery become 0
            if (batLevel < 0)
            {
                light.enabled = false;
                pointLight.enabled = false;
                batLevel = 0;
                isLight = false;
            }
            else
            {
                light.enabled = true;
                pointLight.enabled = true;
                isLight = true;
            }
        }
        else
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player")
             .GetComponent<PlayerController>();
            currTimer -= Time.deltaTime;
            if (player.StressLV < 20)
            {
                if (currTimer < 0)
                {
                    player.StressLV += 1;
                    //timer default 5 seconds
                    currTimer = timer;
                }
            }
            else if (player.StressLV > 20 && player.StressLV < 50)
            {
                if (currTimer < 0)
                {
                    player.StressLV += 1;
                    currTimer = 3;
                }
            }
            else if (player.StressLV >= 50 && player.StressLV < 100)
            {
                if (currTimer < 0)
                {
                    player.StressLV += 2;
                    currTimer = 1;
                }
            }
            else
            {
                player.StressLV = 100;
                //Dead Show Slenderman face
                //TODO
            }
        }

        //get input flashlight button
        if (Input.GetButtonUp("Flashlight") &&
            examines.GetExamineMode == false)
        {
            if (!isLight)
            {
                isLight = true;
                hand.gameObject.SetActive(true);
            }
            else
            {
                isLight = false;
                hand.gameObject.SetActive(false);
            }

        }

        //press reload flashlight
        if (Input.GetButtonUp("Reload"))
        {
            if (batCount > 0)
            {
                batCount -= 1;
                //SetBatLevel to 100;
                this.SetBatLevel = 100f;
            }
            else if (batCount < 0)
            {
                batCount = 0;
            }

        }
    }

    //Getter Battery isLight
    public bool GetIsLight
    {
        get
        {
            return isLight;
        }
    }

    //Getter Battery Light
    public float GetBatLight
    {
        get
        {
            return batLevel;
        }
    }

    //Setter Battery Light
    public float SetBatLevel
    {
        set
        {
            batLevel = value;
        }
    }


    //Getter Battery Count
    public int GetBatCount
    {
        get
        {
            return batCount;
        }
    }

    //Setter Battery Count
    public void SetBatCount()
    {
        batCount++;
    }
}
