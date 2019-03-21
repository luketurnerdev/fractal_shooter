using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    PowerupController powerups;
    Fractal fractal;
    GameController gameController;
	
	void Start ()
    {
		if (GameObject.FindWithTag("PowerupController") != null)
        {
            powerups = GameObject.FindWithTag("PowerupController").GetComponent<PowerupController>();
        }

        if (GameObject.FindWithTag("Layer0") != null)
        {
            fractal = GameObject.FindWithTag("Layer0").GetComponent<Fractal>();
        }

        if (GameObject.FindWithTag("GameController") != null)
        {
            gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }


    }
	
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.F))
        {
            powerups.LaunchSpinShot();
        }

        

        if (Input.GetButtonDown("Fire1"))
        {
            gameController.StartShoot();
        }
    }
}
