using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNormal : MonoBehaviour
{
    [SerializeField] private GameObject door;
    bool isInteract;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isInteract)
        {
        
            if (Input.GetButtonDown("Interact"))
            {
                //play animation door
                door.transform.Rotate(new Vector3(this.transform.rotation.x + 90, this.transform.rotation.y, this.transform.rotation.z));
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = false;
        }
    }
}
