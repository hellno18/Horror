using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float _damageProjectile=20f;
    private float _timer=1f;
    //Global Variable
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //get component AudioManager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>();
   
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = 1f;
            DestroyProjectile();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetHealth = other.GetComponent<PlayerController>() .GetHealth - _damageProjectile;
            audioManager.PlaySE("blood_guts_spill");
            DestroyProjectile();
        }
    }

    

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
