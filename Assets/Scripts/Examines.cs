using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examines : MonoBehaviour
{
    private Camera mainCam;//Camera Object Will Be Placed In Front Of
    private GameObject clickedObject;//Currently Clicked Object

    //Holds Original Postion And Rotation So The Object Can Be Replaced Correctly
    private Vector3 originaPosition;
    private Vector3 originalRotation;

    //If True Allow Rotation Of Object
    private bool examineMode;

    //if true allow to interact
    private bool isInteract;

    //zoom speed
    private float zoomSpeed = 1f;
    float i=0;
    void Start()
    {
        //mainCam = Camera.main;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<Camera>();

        examineMode = false;
        isInteract = false;
    }

    private void Update()
    {
       

        if (isInteract)
        {
            ClickObject();//Decide What Object To Examine

            ScrollObject();

            TurnObject();//Allows Object To Be Rotated

            ExitExamineMode();//Returns Object To Original Postion
        }

    }

    void ScrollObject()
    {
        if (examineMode)
        {
           
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // if object scrolled to front
            if (scroll < 0)
            {
                if (i >= -0.2)
                {
                    clickedObject.transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
                    i += scroll;
                }


            }
            // if object scrolled to back
            else if (scroll > 0)
            {
                if (i <= 0.2)
                {
                    clickedObject.transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
                    i += scroll;
                }
            }
           
        }
    }

    void ClickObject()
    {
        if (Input.GetMouseButtonDown(0) && !examineMode)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Item")
                {
                    //ClickedObject Will Be The Object Hit By The Raycast
                    clickedObject = hit.transform.gameObject;

                    //Save The Original Postion And Rotation
                    originaPosition = clickedObject.transform.position;
                    originalRotation = clickedObject.transform.rotation.eulerAngles;

                    //Now Move Object In Front Of Camera
                    clickedObject.transform.position = mainCam.transform.position 
                        + (transform.forward);

                    //Pause The Game
                    Time.timeScale = 0;

                    //Turn Examine Mode To True
                    examineMode = true;

                }
            }
        }
       
    }

    void TurnObject()
    {
        if (Input.GetMouseButton(0) && examineMode)
        {
            float rotationSpeed = 15;
            //speed scroll mouse
           

            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

           
            clickedObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            clickedObject.transform.Rotate(Vector3.right, yAxis, Space.World);
        }
    }

    void ExitExamineMode()
    {
        if (Input.GetMouseButtonDown(1) && examineMode)
        {
            //Reset Object To Original Position
            clickedObject.transform.position = originaPosition;
            clickedObject.transform.eulerAngles = originalRotation;

            //Unpause Game
            Time.timeScale = 1;

            //Return To Normal State
            examineMode = false;

            //i become 0
            i = 0;
        }
    }

    //unpause 
    public void UnPause()
    {
        //unpause
        Time.timeScale = 1;
    }

    //Getter examineMode 
    public bool GetExamineMode
    {
        get
        {
            return examineMode;
        }
    }

    //Setter ExamineMode
    public void SetExamineMode(bool examine)
    {
        examineMode = examine;
    }

    //setter isinteract
    public void SetIsInteract(bool interact)
    {
        isInteract = interact;
    }



    
}
