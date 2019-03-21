using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    GameObject player;
    Transform ramp;
    Transform lift;

    Transform currentTarget;

    UIController uiCont;
    NavMeshAgent agent;
    FractalSwapper swapper;
    EnemySpawner spawner;
   

    //Knockback lerp functionality

    bool knockedBack;

    bool hitCheckpoint;
    
    public float enemyMoveSpeed;

    
    private GameController gameCont;

    
    void Start ()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        swapper = FindObjectOfType<FractalSwapper>();
        lift = GameObject.FindWithTag("Lift").transform;
        ramp = GameObject.FindWithTag("Ramp").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        uiCont = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        gameCont = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("No agent found.");
           
        }

        else
        {
            if (gameObject.CompareTag("RampEnemy"))
            {
                SetDestination(player.transform);
            }

            else if (gameObject.CompareTag("LiftEnemy"))
            {
                SetDestination(player.transform);
            }
        }
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        
        //Constantly check for obstacles

        if (agent == null)
        {
            Debug.LogError("No agent found.");
        }

        else
        {
            SetDestination(player.transform);
        }





    }

    private void SetDestination(Transform target)
    {
            Vector3 targetVector;
        

        
            targetVector = target.transform.position;
            agent.SetDestination(targetVector);
        
        
    }

    

    public void OnTriggerEnter(Collider other)
    {

        
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            //Do damage
            uiCont.playerHealth -= 1;
            uiCont.UpdatePlayerHealth(uiCont.playerHealth);

            //Knock the enemy back
            gameObject.transform.Translate(0, 0, -3f, Space.Self);
            
            
            
            
           
        }

        if (other.gameObject.CompareTag("SpinShotProjectile"))
        {
            Destroy(gameObject);
            uiCont.UpdateScore();
            spawner.spawnedEnemies--;

            Destroy(other.gameObject);
            

        }

        //Decrease enemy counnt tracker

        

        if (other.gameObject.CompareTag("Layer6"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(6);
            }

            if (gameObject.GetComponent<Fractal>() == null)
            {
                
                Destroy(gameObject);
                uiCont.UpdateScore();
                
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {

                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                    
                }
            }
            
            
        }

        if (other.gameObject.CompareTag("Layer5"))

        {
            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(5);
            }

            if (gameObject.GetComponent<Fractal>() == null)
            {
                
                Destroy(gameObject);
                uiCont.UpdateScore();
               
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                   
                }
            }

        }

        if (other.gameObject.CompareTag("Layer4"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(4);
            }

            if (gameObject.GetComponent<Fractal>() == null)
            {
                
                Destroy(gameObject);
                uiCont.UpdateScore();
                
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                   
                }
            }

        }

        if (other.gameObject.CompareTag("Layer3"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(3);
            }
            if (gameObject.GetComponent<Fractal>() == null)
            {

                spawner.spawnedEnemies--;
                Destroy(gameObject);
                uiCont.UpdateScore();
               
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                    
                }
            }

        }


        if (other.gameObject.CompareTag("Layer2"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(2);
            }
            if (gameObject.GetComponent<Fractal>() == null)
            {
                spawner.spawnedEnemies--;
                Destroy(gameObject);
                
                uiCont.UpdateScore();
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                    
                }
            }

        }


        if (other.gameObject.CompareTag("Layer1"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(1);
            }
            if (gameObject.GetComponent<Fractal>() == null)
            {
                spawner.spawnedEnemies--;
                Destroy(gameObject);
                
                uiCont.UpdateScore();
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    swapper.SwapFractals();
                }

                else
                {
                    Destroy(gameObject);
                    
                }
            }

        }

        if (other.gameObject.CompareTag("Layer0"))

        {

            if (gameCont.removeLayer)
            {
                gameCont.ShedLayers(0);
            }
            if (gameObject.GetComponent<Fractal>() == null)
            {
                spawner.spawnedEnemies--;
                Destroy(gameObject);
                
                uiCont.UpdateScore();
            }

            else
            {
                if (GetComponent<FractalSwapper>() != null)
                {
                    Destroy(gameObject);
                    swapper.SwapFractals();
                    
                }

                else
                {
                    Destroy(gameObject);
                }

            }

        }

        
    }


   


}
