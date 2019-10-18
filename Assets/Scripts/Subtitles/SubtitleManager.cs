using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
  void Start()
  {
    // Typically Begin would be called from the same place that starts the video
    StartCoroutine(FindObjectOfType<SubtitleDisplayer>().Begin());
  }
}
