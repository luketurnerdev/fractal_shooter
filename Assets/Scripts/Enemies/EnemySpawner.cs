using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject fractalEnemy;
    powerupSpawner spawner;
    DirectionSpawner directions;
    public float timer;
    public int wave;
    public int enemyCount;
    private int increment;
    public float spawnInterval;
    public bool spawning = false;

    public Transform[] spawnLocations;

    //Tracking current spawns

  

    public int spawnedEnemies;
    public int spawnedFractalEnemies;
    GameObject[] enemies;
    GameObject[] bosses;

    void Start ()
    {
        spawnedEnemies = 0;
        spawnedFractalEnemies = 0;

        directions = FindObjectOfType<DirectionSpawner>();
        
        spawner = GameObject.FindWithTag("PowerupSpawner").GetComponent<powerupSpawner>();
        wave = 0;
        increment = 1;
        //Initilalize timer as spawn time (e.g., 30 sec), counts down from there
        timer = 0;
	}
	
	
	void Update () {
        timer -= Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (timer <= 0 )
        {
            timer = spawnInterval;
            wave += 1;
            directions.SpawnDirections();
            
            StartCoroutine(SpawnWave(enemyCount));
            spawner.SpawnSpins();
            if (enemyCount < 20)
            {
                enemyCount += increment;
            }
            
        }




       
        
       
	}

    void SpawnSingle()
    {
        Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);

    }

    IEnumerator SpawnWave(int enemyCount)
    {
        spawning = true;

        
        
        //Spawn half the enemies in one location, and half in the other. One second in between spawns.

        if (enemies.Length < 20)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                if (j % 2 != 0)
                {
                    //Spawn one 'super enemy' per 2 rounds
                    //The handling of the enemy 'upgrades' are done in FractalSwapper script.
                    if (spawnedFractalEnemies < 2)
                    {
                        Instantiate(fractalEnemy, fractalEnemy.transform.position - new Vector3(0, 10, 0), fractalEnemy.transform.rotation);
                        spawnedFractalEnemies++;
                    }
                    

                    
                }

                // Spawn normal enemy


                GameObject newEnemy = Instantiate(enemy, spawnLocations[0].position, enemy.transform.rotation);
                spawnedEnemies++;
                newEnemy.tag = "Enemy";
                yield return new WaitForSeconds(1);

                
            }

            spawning = false;
        }
        


    }

    


}
