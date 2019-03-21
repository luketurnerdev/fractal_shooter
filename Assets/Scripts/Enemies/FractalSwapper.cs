using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalSwapper : MonoBehaviour {
    int count;
    EnemySpawner spawner;
    bool changed = false;
    public GameObject twoFractal;
    public GameObject threeFractal;
    UIController uiCont;
    
	
	void Start ()
    {
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        uiCont = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        count = 1;
	}



    public void SwapFractals()
    {
        if (changed == false)
        {
            if (gameObject.name.Contains("1Fractal"))
            {
                Destroy(gameObject);
                uiCont.UpdateScore();
                Instantiate(twoFractal, twoFractal.transform.position, twoFractal.transform.rotation);
                changed = true;
            }

            else if (gameObject.name.Contains("2Fractal"))
            {
                Destroy(gameObject);
                uiCont.UpdateScore();
                Instantiate(threeFractal, threeFractal.transform.position, threeFractal.transform.rotation);
                changed = true;
            }

            else if (gameObject.name.Contains("3Fractal"))
            {
                Destroy(gameObject);
                uiCont.UpdateScore();
                spawner.spawnedFractalEnemies--;
                changed = true;


            }
        }
        

        

        


    }


    void Update ()
    {
		
	}
}
