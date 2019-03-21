using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    float timer;

    public Transform[] spawnLocations;
	
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

		if (timer > 30 )
        {
            timer = 0;
            Spawn(3);
        }


        //Testing section

        if (Input.GetKeyDown(KeyCode.T))
        {
            Spawn(3);
        }
	}

    void Spawn(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            
        }
        
    }
}
