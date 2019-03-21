using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour {
    /*
     This class controls how the powerups affect the player / gameplay.

   

        Spinner: This makes your bullets spin around and shoot projectiles at enemies.

        pseudocode:

        1. Detect pickup of powerup, make the powerup 'collected'
        2. upon next shot, Instantiate pre-made projectile, and Translate it / add force in the direction that it spawned on (e.g., have 4 sides and shoot L,R, forward, back).
        3. Make projectiles kill enemies, or disappear after a given time
    
`       */


    //SpinShot

    public Rigidbody spinShotProjectile;
    public int projectileAmount;



    //Talking to UI

    public UIController uiCont;

    public bool spinShotActive;

    public void CollectSpinShot()
    {
        
        spinShotActive = true;
      

        uiCont.UpdatePowerupText("Spinshot! Press F during shot!");
    }

    public void LaunchSpinShot()
    {
        StartCoroutine(SpinShot());
    }

    public IEnumerator SpinShot()
    {
        if (spinShotActive)
        {

            if (GameObject.FindWithTag("Layer0") != null)
            {
                GameObject bullet = GameObject.FindWithTag("Layer0");

                //Make a new array of directions, then use these to add force to newly spawned bullets.
                
                Vector3[] definedVectors = new Vector3[projectileAmount];
                
                definedVectors[0] = Vector3.forward;
                definedVectors[1] = Vector3.left;
                definedVectors[2] = Vector3.back;
                definedVectors[3] = Vector3.right;

                int count = 0;

                
                
                for (int i = 0; i < projectileAmount; i++)
                    

                {
                    Rigidbody newSpinShot;
                    newSpinShot = Instantiate(spinShotProjectile, bullet.transform.position , Quaternion.Euler(90,0,0)) as Rigidbody;
                    newSpinShot.AddForce(definedVectors[count] * 750);
                    if (count < 3)
                    {
                        
                        count++;

                    }

                    else
                    {
                        count = 1;
                    }

                   
                    
                   

                    newSpinShot.tag = "SpinShotProjectile";
                    

                    if (spinShotActive)
                    {
                        uiCont.powerupText.text = "";
                        spinShotActive = false;
                       
                    }
                    
                    yield return new WaitForSeconds(0.07f);
                   
                }

                //Cleanup
                


                GameObject[] spinshots = (GameObject.FindGameObjectsWithTag("SpinShotProjectile"));
                for (int z = 0; z < spinshots.Length; z++)
                {
                    Destroy(spinshots[z]);
                }

                

               



            }
            

        }

}





    void Start ()
    {
        spinShotActive = false;
	}


    void Update()
    {

    }
    }
