using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    private PlayerController player;      // Reference to the player's .
    [SerializeField] private GameObject enemy;             // The enemy prefab to be spawned.
    [SerializeField] private Transform spawnPoints;  // the spawn points this enemy can spawn from.
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    void Spawn()
    {
        // If the player has no health left...
        if (player.GetHealth <= 0f)
        {
            // ... exit the function.
            return;
        }
        // Create an instance of the enemy prefab at spawn point's position and rotation.
        Instantiate(enemy, spawnPoints.position, spawnPoints.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        //call spawn function and destroy this gameobject
        Spawn();
        Destroy(this.gameObject);
    }
}
