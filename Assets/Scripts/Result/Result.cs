using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Result : MonoBehaviour
{
    Transform gameOverText;
    Transform winText;
    Image bg;
    AudioManager audioManager;
   

    // Start is called before the first frame update
    void Start()
    {
        gameOverText = this.transform.Find("GameOverText");
        winText = this.transform.Find("WinText");
        bg = this.transform.GetComponent<Image>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Result") == "Win")
        {
            winText.gameObject.SetActive(true);
            bg.color = Color.black;
        }

        if (PlayerPrefs.GetString("Result") == "Lose")
        {
            gameOverText.gameObject.SetActive(true);
            bg.color = Color.red;
            audioManager.PlayBGM("Mystery-David-Fesliyan");
        }
    }
}
