using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    bool hold;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            hold = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            hold = false;
        }
        if (hold == false)
        {
            this.transform.parent = null;
            //Debug.Log("released");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && hold)
        {
            this.transform.parent = other.transform;
            //Debug.Log("holding");
        }
    }
}
