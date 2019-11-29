using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnV2 : MonoBehaviour
{
    [SerializeField] private GameObject enemy;                // The enemy prefab to be spawned.
    private float intervalTime = 10f;            // How long between each spawn.
    private float startTime = 2f;
    private PlayerController playerHealth;       // Reference to the player's heatlh.
    private Transform player;            // The position that that camera will be following.
    private FlashLight flashlight;       //reference to the player flashlight
    private float spawnDistance = 13f;    //reference between the player and enemy

    private void Start()
    {
        /******************
         * GET COMPONENT 
         ******************/
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        flashlight = player.GetComponentInChildren<FlashLight>();

        //gameobject children set into false
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", startTime, intervalTime);

    }

    private void Spawn()
    {
        // If the player has no health left...
        if (playerHealth.GetHealth <= 0f )
        {
            // ... exit the function.
            return;
        }


        // collect the children that are close.
        List<Transform> near = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform spawnPoint = transform.GetChild(i);
            //debugger
            //print(Vector3.Distance(player.transform.position, spawnPoint.position)+"xx"+ transform.GetChild(i));
            if (Vector3.Distance(player.transform.position, spawnPoint.position) <= spawnDistance)
            {
                near.Add(spawnPoint);
            }
        }

        if (near.Count > 0)
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, near.Count);

            //while player not used flashlight, enemy will come from detect spot(near player) 
            if (!flashlight.GetIsLight)
            {
                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                GameObject instance = (GameObject)Instantiate(enemy, near[spawnPointIndex].position, near[spawnPointIndex].rotation);
                instance.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
            }
           
        }
    }
}
