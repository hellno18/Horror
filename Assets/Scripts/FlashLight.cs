using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private bool isLight;
    private float batLevel=50f;
    private int batCount=0;
    private float damage=0.5f;
    private bool delayDamage = false;
    private Transform hand;
    private Camera mainCamera;
    private EnemySlender enemySlender;
    private Examines examines;
    
    // Start is called before the first frame update
    void Start()
    {
        //light off
        isLight = false;
        //find hand component
        hand = this.transform.Find("Hand");
        mainCamera = this.transform.GetComponent<Camera>();
        //get component examines
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(batLevel);
        //if turn on light will decrease
        if (isLight)
        {
            //decrease light power
            batLevel -= Time.deltaTime;

            //Raycast to Gameobject
            RaycastHit hit;
            if(Physics.Raycast(this.mainCamera.transform.position,
                this.mainCamera.transform.forward,out hit))
            {
                //Give damage to enemy with flashlight
                if (hit.collider.tag == "Slender")
                {
                    enemySlender= hit.collider.transform.GetComponent<EnemySlender>();
                    StartCoroutine("DamageCoroutine");
                    if(delayDamage) enemySlender.DamageHitEnemy(damage);
                }
            }
            
            Light light = GameObject.Find("Spotlight")
                .GetComponent<Light>();
            Light pointLight = GameObject.Find("PointLight")
                .GetComponent<Light>();
            //turn off the light while power battery become 0
            if (batLevel<0)
            {
                light.enabled=false;
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
            StartCoroutine(StressLVUpCoroutine());
        }

        //get input flashlight button
        if (Input.GetButtonUp("Flashlight")&&
            examines.GetExamineMode==false)
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

    //Damage delay coroutine
    IEnumerator DamageCoroutine()
    {
        delayDamage = true;
        yield return new WaitForSeconds(0.3f);
        delayDamage = false;
    }

    //StressLV UP Coroutine
    IEnumerator StressLVUpCoroutine()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
        yield return new WaitForSeconds(2f);
        player.StressLV += 1;
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
