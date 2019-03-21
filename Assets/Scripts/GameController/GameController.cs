using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    


    //Controllers

    PowerupController powerups;
    UIController uiCont;
    GameController gameCont;

    //Fractals
    public Fractal fractal;
    public GameObject fractalPrefab;
    Fractal prefabScript;
    Transform bulletTransform;
    private bool fractalExists;
    private GameObject topFractal;
    private bool fractalAssigned;

    //Gun / Bullets

    public GameObject bulletEmitter;
    public Rigidbody bulletFractal;
    public float bulletForce;
    public Animator gunAnim;
    public float bulletDuration;
    public AudioSource gunSounds;
    

    //Shooting
    public float shootInterval;
    public static bool shootReady;
    private bool shooting;

    //Gun Colours
    public Material gunMat;

    //Assigning meshes

    private Mesh[] meshes;

    //Removing layers

    public bool removeLayer = true;
    public float timeBetweenLayers;

    //Directionality

    public enum Direction { Up = 1 , Down, Left, Right, Forward, Backward};
    public int directionsCollected;

    //Rotation
    public float rotationSpeed;
    bool axisSelected = false;
    int rotationAxis;
    enum Rotation {X, Y, Z };

    void RotateFractal()
    {
        if (GameObject.FindWithTag("Layer0")!= null)
        {
            List<GameObject> currentFractals;
            currentFractals = new List<GameObject>();

            // Find currently spawned bullets

            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Layer0"))
            {
                currentFractals.Add(obj);
            }

            
           

            //Add rotation in random direction
            foreach (GameObject bullet in currentFractals)
            {
                if (!axisSelected)
                {
                    rotationAxis = Random.Range((int)Rotation.X, (int)Rotation.Z);
                    axisSelected = true;
                }


                if (rotationAxis == (int)(Rotation.X))
                {
                    bullet.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
                    
                }

                if (rotationAxis == (int)(Rotation.Y))
                {
                    bullet.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                    
                }

                if (rotationAxis == (int)(Rotation.Z))
                {
                    bullet.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                    
                }
            }
            
            

            

        }


       

        else
        {
            topFractal = null;
            axisSelected = false;
        }
        
    }
    
    void RotateEnemyFractals()
    {
        if (GameObject.FindWithTag("Enemy") != null)
        {


            List<GameObject> currentFractals;
            currentFractals = new List<GameObject>();

            // Find currently spawned enemyFractals

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (obj.GetComponent<Fractal>() != null)
                {
                    currentFractals.Add(obj);
                }

            }



            //Add rotation

            foreach (GameObject enemy in currentFractals)
            {
                if (!axisSelected)
                {
                    rotationAxis = (int) Rotation.Y;
                    axisSelected = true;
                }


                

                if (rotationAxis == (int)(Rotation.Y))
                {
                    enemy.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

                }

                
            }





        }
    }

   
    void ChangeGunColour()
    {

        if (GameObject.FindWithTag("Gun") != null)
        {
            if (shootReady)
            {
                gunMat.color = Color.green;
            }

            else
            {
                gunMat.color = Color.red;
            }
        }
        
    }

    void Start()
    {
        //Assigning references
        bulletTransform = fractalPrefab.GetComponent<Transform>();
        powerups = GameObject.FindWithTag("PowerupController").GetComponent<PowerupController>();
        uiCont = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        gameCont = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        //Scale

        bulletTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        //Direction - Initialize as 0 directions spawning on bullets
        prefabScript = fractalPrefab.GetComponent<Fractal>();

        for (int i = 0; i < prefabScript.directionSelected.Length; i++)
        {
            prefabScript.directionSelected[i] = false;

         }
        //Shooting
        shootReady = true;
        shooting = false;
        
        
       
        
    }

    


    void Update()
    {
        //Change gun color to indicate if shot is ready to fire
        ChangeGunColour();
        RotateFractal();
        RotateEnemyFractals();
        spawnFractalAudio();
        
        
    }

    public void AddDirection(int direction)
    {
        switch (direction)
        {
            case 0:
                prefabScript.directionSelected[0] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected += 1;
                break;

            case 1:
                prefabScript.directionSelected[1] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected += 1;
                break;

            case 2:
                prefabScript.directionSelected[2] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected += 1;
                break;

            case 3:
                prefabScript.directionSelected[3] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected += 1;
                break;

            case 4:
                prefabScript.directionSelected[4] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected += 1;
                break;

            case 5:
                prefabScript.directionSelected[5] = true;
                bulletTransform.localScale = (bulletTransform.localScale + new Vector3(0.1f, 0.1f, 0.1f));
                directionsCollected +=1;
                break;
        }

        uiCont.UpdateDirectionText(directionsCollected);
    }




    public void StartShoot()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        //Instantiate a bullet, using the Fractal class. It is a rigidbody so that it can have force added.
        //Destroys itself after a certain amount of time

        if (shootReady == true)
        {
            shooting = true;
            shootReady = false;
            Rigidbody bulletInstance;
            bulletInstance = Instantiate(bulletFractal, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as Rigidbody;
            bulletInstance.GetComponent<Fractal>().enabled = true;
            fractal = bulletInstance.GetComponent<Fractal>();

            //Add force
            bulletInstance.AddForce(bulletEmitter.transform.forward * bulletForce);
            Destroy(bulletInstance.gameObject, bulletDuration);

            





            //Gun shooting animation

            if (shooting)
            {
                gunAnim.Play("ShootAnim");
                gunSounds.Play();
            }

            //waits before allowing a shot to be fired again

            yield return new WaitForSeconds(shootInterval);
            shootReady = true;
        }
        

    }

    

    public void ShedLayers(int layer)
    {
        Debug.Log("shedding " + layer);
        //<summary>

        //This function makes a list of all layers with a certain tag, and destroys them on impact. 
        //It then waits a short amount of time before enabling again, so that multiple layers aren't destroyed at once.

        //</summary>

       
        switch (layer)
        {
            case 0:

                 
                    if (GameObject.FindWithTag("Layer1") == null)
                    {
                        
                        GameObject[] layer0List = GameObject.FindGameObjectsWithTag("Layer0");
                        for (int i = 0; i < layer0List.Length; i++)
                        {
                            Destroy(layer0List[i]);
                            
                        }
                    removeLayer = false;
                    StartCoroutine(ResetLayers());



                    uiCont.UpdateScore();
                    }

                
                    


                

                

                break;
            case 1:
                
                
                    
                    GameObject[] layer1List = GameObject.FindGameObjectsWithTag("Layer1");
                    for (int i = 0; i < layer1List.Length; i++)
                    {
                        Destroy(layer1List[i]);
                        

                    }
                removeLayer = false;
                StartCoroutine(ResetLayers());


                uiCont.UpdateScore();
                    

                

               

                break;


            case 2:
                
                    removeLayer = false;
                    GameObject[] layer2List = GameObject.FindGameObjectsWithTag("Layer2");
                    
                    for (int i = 0; i < layer2List.Length; i++)
                    {

                        Destroy(layer2List[i]);
                        

                    }
                removeLayer = false;
                StartCoroutine(ResetLayers());

                uiCont.UpdateScore();


                
              



                break;

            //Enemy collided with layer 3
            case 3:

               
                    
                    GameObject[] layer3List = GameObject.FindGameObjectsWithTag("Layer3");
                    
                    for (int i = 0; i < layer3List.Length; i++)
                    {
                        Destroy(layer3List[i]);
                        
                    }
                removeLayer = false;
                StartCoroutine(ResetLayers());
                    uiCont.UpdateScore();
                    





                break;

            case 4:
                
               
                    
                    GameObject[] layer4List = GameObject.FindGameObjectsWithTag("Layer4");
                for (int i = 0; i < layer4List.Length; i++)
                {
                    Destroy(layer4List[i]);


                }
                removeLayer = false;
                StartCoroutine(ResetLayers());

                uiCont.UpdateScore();


                    
                
                
                break;

            case 5:
                
                
                    
                    GameObject[] layer5List = GameObject.FindGameObjectsWithTag("Layer5");
                    for (int i = 0; i < layer5List.Length; i++)
                    {
                        Destroy(layer5List[i]);
                        
                    }
                removeLayer = false;
                StartCoroutine(ResetLayers());

                uiCont.UpdateScore();


                    
               
                
                break;

            case 6:

                
                   
                    GameObject[] layer6List = GameObject.FindGameObjectsWithTag("Layer6");
                    for (int i = 0; i < layer6List.Length; i++)
                    {
                        Destroy(layer6List[i]);
                        
                    }
                removeLayer = false;
                StartCoroutine(ResetLayers());


                uiCont.UpdateScore();


                    
                
                
                    break;

            

        }

        


    }

    IEnumerator ResetLayers()
    {
        


        yield return new WaitForSeconds(1f);
        removeLayer = true;
    }

    
     

    void spawnFractalAudio()
    {
        GameObject[] f = GameObject.FindGameObjectsWithTag("Layer0");
        foreach(GameObject o in f)
        {
            if(o.GetComponent<FractalAudio>()==null)

            {
                o.AddComponent<FractalAudio>();

            }
        }

    }

}