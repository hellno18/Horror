using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private bool isLight;
    private float batLevel=50f;
    private int batCount=0;
    Transform hand;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //light off
        isLight = false;
        //find hand component
        hand = this.transform.Find("Hand");
        mainCamera = this.transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(batLevel);
        //if turn on light will decrease
        if (isLight)
        {
            //decrease light power
            batLevel -= Time.deltaTime;

            //Raycast to Gameobject
            RaycastHit hit;
            if(Physics.Raycast(this.mainCamera.transform.position,this.mainCamera.transform.forward,out hit))
            {
                //Give damage to enemy with flashlight
                if (hit.collider.tag == "Slender")
                {
                    Debug.Log("DAMAGE");
                }
            }
            
            Light light = GameObject.Find("Spotlight").GetComponent<Light>();
            Light pointLight = GameObject.Find("PointLight").GetComponent<Light>();
            //turn off the light while power battery become 0
            if (batLevel<0)
            {
                light.enabled=false;
                pointLight.enabled = false;
                batLevel = 0;
            }
            else
            {
                light.enabled = true;
                pointLight.enabled = true;
            }
        }

        //get input flashlight button
        if (Input.GetButtonUp("Flashlight"))
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
                SetBatLevel(100f);
            }
            else if (batCount < 0)
            {
                batCount = 0;
            }
            
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
    public void SetBatLevel(float batt)
    {
        batLevel = batt;
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
