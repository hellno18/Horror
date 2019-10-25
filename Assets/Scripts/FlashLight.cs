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
    Light pointLight;
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
        pointLight = GameObject.Find("PointLight")
    .GetComponent<Light>();
    }

    //OverLoading flashlight
    protected override void FlashLightRun()
    {
        //if turn on light will decrease
        if (isLight)
        {
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
            //turn off the light while power battery become 0
            if (batLevel < 0)
            {
                light.enabled = false;
                pointLight.enabled = false;
                batLevel = 0;
                hand.gameObject.SetActive(false);
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
            //pointlight disable
            pointLight.enabled = false;
           
        }

        //get input flashlight button
        if (Input.GetButtonUp("Flashlight") &&
            examines.GetExamineMode == false)
        {
            if (!isLight&&batLevel>0)
            {
                isLight = true;
                hand.gameObject.SetActive(true);
                pointLight.enabled = true;
            }
            else
            {
                isLight = false;
                hand.gameObject.SetActive(false);
                pointLight.enabled = false;
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
