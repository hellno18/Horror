using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGenerator : MonoBehaviour
{
    ElectroPanel electro;
    HUD hud;
    bool isInteract;
    bool isOpen;
    // Smoothly open a door
    [SerializeField] private float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float openTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //get component electropanel
        electro = GameObject.Find("ElectoPanel/DetectElectro")
            .GetComponent<ElectroPanel>();
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        defaultRotationAngle = transform.localEulerAngles.x;
        currentRotationAngle = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle +
            (isOpen ? doorOpenAngle : 0), openTime),transform.localEulerAngles.y 
            , transform.localEulerAngles.z);
        if (electro.GetIsDoor)
        {
            //electro true
            if (Input.GetButtonDown("Interact") && isInteract)
            {
                isOpen = !isOpen;
                currentRotationAngle = transform.localEulerAngles.x;
                openTime = 0;
            }
        }
    }

    /*=============================
    * While collision trigger enter
    ==============================*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = true;
            if (!electro.GetIsDoor)
            {
                //Show hud "broken"
                print("Broken");
            }
            else
            {
                //show hud press interact
                StartCoroutine(CButtonCoroutine());
            }
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteract = false;
            if (!electro.GetIsDoor)
            {
                //Show hud "broken"
            }
        }
    }

    IEnumerator CButtonCoroutine()
    {
        hud.CButtonDisplay();
        yield return new WaitForSeconds(1f);
        hud.CButtonDisplay();
    }
}
