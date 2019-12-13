using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    //Global Variable
    AudioManager audioManager;
  void Start()
  {
        // Typically Begin would be called from the same place that starts the video
        StartCoroutine(FindObjectOfType<SubtitleDisplayer>().Begin());
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
        audioManager.PlayBGM("The_Dark_Castle_-_David_Fesliyan");
  }
}
