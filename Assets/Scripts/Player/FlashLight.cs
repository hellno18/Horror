using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : PlayerBase
{
    private bool isBringTorch;          // Whether player is bring torch
    private bool isLight;               // Whether player is turned on light
    private bool isReload;              // Whether player is reload battery
    private float batLevel = 50f;          // The amount of battery level.
    private int batCount = 0;              // The amount of battery count.
    private float distance = 15f;  //The distance in float between player and enemy.
    private float damage = 0.5f;      //damage flashlight.

    private float timer = 5;              // Timer
    private float currTimer;            //current timer
    private float reloadTimer = 5;

    //Global Variable
    private Transform hand;         // Reference to hand component.
    private Camera mainCamera;      // Reference to camera component.
    private Examines examines;               // Reference to examines component.
    private Light pointLight;                // Reference to light component.
    private AudioManager audioManager;
    private Image reloadUI;
    // Start is called before the first frame update
    void Start()
    {
        //bringTorch false
        isBringTorch = false;
        currTimer = timer;
        //light off
        isLight = false;
        //reload false
        isReload = false;
        //Get ReloadUI
        reloadUI = hud.transform.Find("ReloadUI").GetComponent<Image>();

        //find hand component
        hand = this.transform.Find("Hand");
        mainCamera = this.transform.GetComponent<Camera>();
        //get component examines
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
        pointLight = GameObject.Find("PointLight").GetComponent<Light>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
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
                //Give damage to enemy with flashlight (slender patrol)
                if (hit.collider.tag == "SlenderPatrol")
                {
                    EnemySlenderNormal enemySlenderPatrol = hit.collider.transform.GetComponent<EnemySlenderNormal>();
                    var damageTarget = enemySlenderPatrol.GetComponent<IDamage>();
                    if (damageTarget != null)
                        damageTarget.AddDamageEnemy(damage);
                    enemySlenderPatrol.GetComponent<Animator>().SetTrigger("Hit");
                }
                //Give damage to enemy with flashlight (slender spawn)
                else if (hit.collider.tag == "SlenderSpawn")
                {
                    EnemySlenderNormal enemySlenderNormal = hit.collider.transform.GetComponent<EnemySlenderNormal>();
                    var damageTarget = enemySlenderNormal.GetComponent<IDamage>();
                    if (damageTarget != null)
                        damageTarget.AddDamageEnemy(damage);
                    enemySlenderNormal.GetComponent<Animator>().SetTrigger("Hit");
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
                //Play turn off torch
                //audioManager.PlaySE("torch");
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
            !examines.GetExamineMode && isBringTorch)
        {
            //Play se
            audioManager.PlaySE("torch");
            if (!isLight && batLevel > 0)
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

        
        
        
        if (isReload)
        {
            reloadTimer -= Time.deltaTime;
            reloadUI.fillAmount = reloadTimer/5;
            if (reloadTimer<0)
            {
                reloadTimer = 0;
                reloadUI.fillAmount = 0;
            }
        }
        else if(!isReload)
        {
            reloadTimer = 5;
            reloadUI.fillAmount = reloadTimer/5;
            reloadUI.gameObject.SetActive(false);
        }
        print(reloadTimer);
        /*====================
        *press reload flashlight
        =====================*/
        if (Input.GetButtonUp("Reload")&& batCount>0 && IsBringTorch)
        {
            //give delay 5 second
            StartCoroutine(ReloadCoroutine(reloadUI));
            
            

        }
    }

    private IEnumerator ReloadCoroutine(Image reloadUI)
    {
        isReload = true;
        reloadUI.gameObject.SetActive(true);
        //play se
        audioManager.PlaySE("change_battery");
        yield return new WaitForSeconds(reloadTimer);
        //random reload
        //Battery 80-100%
        int randomBat = Random.Range(80, 100);
        if (batCount > 0 && reloadTimer <= 0)
        {
            batCount -= 1;
            //SetBatLevel to 80-100;
            this.SetBatLevel = randomBat;
            isReload = false;
        }
        else if (batCount < 0)
        {
            batCount = 0;
        }
    }

    /*========================
    *Setter isBringTorch
    ========================*/
    public bool SetIsBringTorch
    {
        set
        {
            isBringTorch = value;
        }
    }

    /*========================
     *Getter Battery isLight
     ========================*/
    public bool GetIsLight
    {
        get
        {
            return isLight;
        }
    }

    /*====================
    *Getter Battery Light
    =====================*/
    public float GetBatLight
    {
        get
        {
            return batLevel;
        }
    }

    /*====================
    *Setter Battery Light
    =====================*/
    public float SetBatLevel
    {
        set
        {
            batLevel = value;
        }
    }


    /*====================
    *Getter Battery Count
    =====================*/
    public int GetBatCount
    {
        get
        {
            return batCount;
        }
    }

    /*====================
    *Setter Battery Count
    =====================*/
    public void SetBatCount()
    {
        batCount++;
    }

    public bool IsBringTorch{
        get
        {
            return isBringTorch;
        }
    }
}
