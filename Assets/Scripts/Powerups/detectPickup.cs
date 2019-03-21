using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPickup : MonoBehaviour {
    PowerupController powerups;
    powerupSpawner spawner;

    void Start()
    {
        powerups = GameObject.FindWithTag("PowerupController").GetComponent<PowerupController>();
        spawner = GameObject.FindWithTag("PowerupSpawner").GetComponent<powerupSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (this gameobject) is a spinshot, run a function that collects the powerup

        if (gameObject.CompareTag("spinShot"))
        {
            if (other.CompareTag("Player"))
            {

                if (!powerups.spinShotActive)
                {
                    powerups.CollectSpinShot();
                    Destroy(gameObject);
                }
                

                

            }
        }

    }
}
