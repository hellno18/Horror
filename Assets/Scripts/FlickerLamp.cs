using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLamp : MonoBehaviour
{
    Light light;
    float maxIntensity=4f;
    float minIntensity=0f;
    [Range(1, 50)]
    int smoothing = 5;
    Queue<float> smoothQueue;
    float lastSum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = this.GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //random timer 
        float timer = Random.Range(0, 5);
        timer -= Time.deltaTime;

        if (light == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }
       
        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        if (timer < 0)
        {
            timer = Random.Range(0, 3);
            StartCoroutine("LightCoroutine");
        }
    }

    IEnumerator LightCoroutine()
    {
        yield return new WaitForSeconds(2f);
        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;
    }
    void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }
}
