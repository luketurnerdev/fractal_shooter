using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupSpawner : MonoBehaviour {
    public EnemySpawner enemySpawner;
    public GameObject spinShotPowerup;
    public Transform[] spawnLocations;
    powerupSpawner spawner;
    

    //Features:

    // spawn locations (randomized?)
    // amount of spawns
    //Maybe we can make a certain amount of them ( maybe 2) spawn throughout the 30 sec timer.
    //If 2 exist, don't spawn any more.


    private float amountPerWave;
    public float spinShotsSpawned;
    private float waitTime;
    GameObject[] powerupList;
	
	void Start ()
    {
        powerupList = new GameObject[0];
        amountPerWave = 2;
        
        spawner = GameObject.FindWithTag("PowerupSpawner").GetComponent<powerupSpawner>();
        
        

    }
	
    //Each wave, 6 arrows will spawn.
	
	void Update ()
    {
        powerupList = GameObject.FindGameObjectsWithTag("spinShot");
       

        spinShotsSpawned = powerupList.Length;
        


    }

    public void SpawnSpins()
    {
        if (spinShotsSpawned <= 4)
        {
            StartCoroutine(SpawnSpinShots());
        }
        
    }

    IEnumerator SpawnSpinShots()
    {
        
            waitTime = Random.Range(0, 15);

            yield return new WaitForSeconds(waitTime);
            

            Instantiate(spinShotPowerup, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity);
            
        
        
        

        






    }
}
