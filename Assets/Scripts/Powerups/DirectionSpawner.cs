using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSpawner : MonoBehaviour {
    //The user should have one direction spawn per wave


    //Order - Up, Down, Left, Right, Forward, Back
   
    public Transform[] spawnLocations;
    public GameObject[] directions;
    int directionsSpawned;
    int waitTime;

    void Start()
    {
        directionsSpawned = 0;
    }

    

    public void SpawnDirections()
    {
        waitTime = Random.Range(0, 25);
        
        if (directionsSpawned <= 6)
        {
            
            
            
            Instantiate(directions[directionsSpawned], spawnLocations[directionsSpawned].position, Quaternion.identity);
            directionsSpawned += 1;
        }

        
    }

    
	
	
	void Update ()
    {
		
	}
}
