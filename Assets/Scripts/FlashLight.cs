using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    bool isLight;
    GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        isLight = false;
        hand = GameObject.Find("Hand").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Flashlight")!=0)
        {
            isLight = true;
            hand.SetActive(false);
        }
    }
}
