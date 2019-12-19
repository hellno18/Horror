using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Normal key, special for ending game
    enum KeyType
    {
        normal,
        special
    }

    [SerializeField] private KeyType keyType = KeyType.normal;

    bool isInteract;

    //Global Variable
    HUD hud;
    Examines examines;
    AudioManager audioManager;

    // Start is called before the first frame update
    private void Awake()
    {
        //get component HUD
        hud = GameObject.Find("CanvasHUD").GetComponent<HUD>();
        //get component examine
        examines = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Examines>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        isInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Grab")&& isInteract)
        {
            //play SE
            audioManager.PlaySE("pickitem");

            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<IKey>();
            if (examines.GetExamineMode)
            {
                examines.UnPause();
                examines.SetExamineMode(false);
            }
            
            switch (keyType)
            {
                case KeyType.normal:
                    //Add key normal
                    player.AddKeyNCount();
                    break;
                case KeyType.special:
                    //Now player can exit from the game
                    player.SetKeyExit(true);
                    break;
            }
            
            
           //display hud
            hud.GButtonDisplay();
            //destroy
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact true
            examines.SetIsInteract(true);
            isInteract = true;
        }
    }

    /*=============================
    * While collision trigger exit
    ==============================*/
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.GButtonDisplay();
            //interact false
            examines.SetIsInteract(false);
            isInteract = false;
        }
    }
}
