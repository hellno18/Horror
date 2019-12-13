using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGenerator : MonoBehaviour
{
    public bool IsClear { get; set; }
    bool isInteract;
    bool isOpen;

    // Smoothly open a door
    [SerializeField] private float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float openTime = 0;
    private float timer = 2;
    //Global Variable 
    ElectroPanel electro;
    AudioManager audioManager;
    HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        //get component electropanel
        electro = GameObject.Find("ElectoPanel/DetectElectro")
            .GetComponent<ElectroPanel>();
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component audiomanager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
        IsClear = false;
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
                IsClear = true;
                isOpen = !isOpen;
                if (isOpen) audioManager.PlaySE("open_creaky_door");
                else audioManager.PlaySE("door_creak_closing");
                currentRotationAngle = transform.localEulerAngles.x;
                openTime = 0;

            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;
            if (Input.GetButtonDown("Interact") && isInteract && timer==0)
            {
                timer = 2;
                audioManager.PlaySE("door_knob");
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
                StartCoroutine(hud.CButtonCoroutine());
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


}
