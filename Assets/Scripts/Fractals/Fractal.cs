using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    //Used to store attributes of the MeshFilter and MeshRenderer components

    //Mesh draws complex shapes. (3D)
    //Renderer makes it show up on screen.

    // Affects how small the child will be, compared to the parent.

    public float childScale;

    public Mesh[] meshes;
    public Material material;
    public int maxDepth;
    private int depth;
    public float waitTime;
    public bool randomWaitTime;
    public float spawnProbability;
    private bool fractalExists;
    public bool randomMesh;

    bool safeToDelete;




    //Heirarchy control

    public static bool fractalComplete = false;
    public static GameObject topFractal;

    //Enemy Fractals
    private bool isEnemy ;

    //Optimization

    private bool scriptsDeleted;

    
    //The comma in the index box indicates that this array has two dimensions (x and y for example).
    private Material[,] materials;


    void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
        //Optimization tools

        scriptsDeleted = false;

        //Finding fractal at top of heirarchy
        
        topFractal = GameObject.FindGameObjectWithTag("Layer0");

        //Assigning layer tags
        //If this instance of the Fractal script is being used on an enemy, assign its tag as such.
        //Otherwise, assign tags based on the current layer (1-6).

        if (!isEnemy)
        {
            switch (depth)
            {
                case 0:

                    tag = "Layer0";
                    
                    break;

                case 1:
                    if (transform.parent.tag != "Enemy")
                    {
                        tag = "Layer1";
                    }

                    else
                    {
                         tag = "Enemy";
                        
                    }

                    break;

                case 2:
                    if (transform.parent.parent.tag != "Enemy")
                    {
                        tag = "Layer2";
                    }

                    else
                    {
                         tag = "Enemy";
                        

                    }
                    break;

                case 3:
                    if (transform.parent.parent.parent.tag != "Enemy")
                    {
                        tag = "Layer3";
                    }

                    else
                    {
                         tag = "Enemy";
                       
                    }

                    break;

                case 4:

                    if (transform.parent.parent.parent.parent.tag != "Enemy")
                    {
                        tag = "Layer4";
                    }

                    else
                    {
                        tag = "Enemy";
                    }

                    break;

                case 5:
                    tag = "Layer5";
                    break;

                case 6:
                    tag = "Layer6";
                    break;
            }
        }
       

        
        if (materials == null)
        {
            InitializeMaterials();

        }
        

        /// Adding 2 new components, and assigning our variables to them
        //This happens everytime a new <Fractal> is spawned, and its Start() runs

        //The material is retrieved from the materials[] array, with an index argument of the current depth level.

        if (randomMesh)
        {
            //Spawn a random number of meshes between 0 and length of meshes
            gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        }

        else
        {
            gameObject.AddComponent<MeshFilter>().mesh = meshes[0];
        }
        

      
         //Meshrenderer now takes 2 arguments in the index to randomly switch between 2 color cycles

        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];

        //This ensures the program will not infinitely loop

        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
}

    void Update()
    {

    
    

    
      
    }

   

    
    void DeleteDeadScripts()
    {
        switch (maxDepth)
        {
          case 3:

                GameObject[] layer3 = GameObject.FindGameObjectsWithTag("Layer3");
                if (layer3.Length >= directionSelected.Length * maxDepth) 
                {
                   
                    GameObject[] layer1 = GameObject.FindGameObjectsWithTag("Layer1");

                    //Only delete unused scripts if the layer has fully spawned
                    Debug.Log("layer3 contains: " + layer3.Length);
                   
                    

                    for (int i = 0; i < layer1.Length; i++)
                    {
                        Destroy(layer1[i].GetComponent<Fractal>());
                        
                    }

                    GameObject[] layer2 = GameObject.FindGameObjectsWithTag("Layer2");

                    for (int i = 0; i < layer2.Length; i++)
                    {
                        Destroy(layer2[i].GetComponent<Fractal>());

                    }

                }
                break;
        }



        scriptsDeleted = true;










    }
    
   

    

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1, 2];

        //Change color of MeshRenderer
        //by Lerping between white and blue over a float determined by
        //progress through the generations (out of maxDepth).

        for (int i = 0; i <= maxDepth; i++)
        {

            //from 0 until maxdepth (eg 4), increase i
            // t = 0 / (4-1)
            //

            float t = i / (maxDepth - 1f);
            t *= t;

            //1st dimension of array

            materials[i, 0] = new Material(material);          //Current material index gets new material
            materials[i, 0].color =                            // Color changes once per change in maxDepth (i).
                Color.Lerp(Color.cyan, Color.blue, i / maxDepth);

            //2nd dimension

            materials[i, 1] = new Material(material);          //Current material index gets new material
            materials[i, 1].color =                            // Color changes once per change in maxDepth (i).
                Color.Lerp(Color.white, Color.cyan, i / maxDepth);
        }
        //Set the deepest level to have a very different colour

        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.yellow;
    }

    //Letting user handle directionality

    public bool[] directionSelected;


    //Directional tools//
    private static Vector3[] childDirections =
    {
        // These indexes are used to generate both the direction and rotation of children [i].
        //Corresponds to array indexes 0 to 5.
        //Future: could we lerp between different Vectors to make custom vectors?
        //These vectors could lerp along different types of curves?

        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
        Vector3.down
    };

    //Rotational tools

    private static Quaternion[] childOrientations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f),
        Quaternion.Euler(0f,0f,90f)

    };




    

    private IEnumerator CreateChildren()
    {


        //<summary>

        //Make a new child object, and add an instance of this script
        // 'this' is the argument that refers to the current fractal,
        // and all its references are assigned in Initialize().

        //</summary>


        //This for loop iterates through the amount of elements in the childDirections[] array (Vector3).


        
        for (int i = 0; i < childDirections.Length; i++)
        {

        if (Random.value < spawnProbability)
            {
                
                

                if (directionSelected[i] == true)
                {
                    
                    //Randomizing delay with Random.Range

                    yield return new WaitForSeconds(waitTime);

                    //Instantiate a new child
                    GameObject fractalChild = new GameObject("Fractal Child");
                    fractalChild.AddComponent<Fractal>().Initialize(this, i);
                    fractalChild.AddComponent<BoxCollider>().isTrigger = true;
                    
                }
                

                
            }

        }
        
}

    public void Initialize(Fractal parent, int childIndex)
    {
        //Assigning local variable values from parent fractal
        if (randomWaitTime)
        {
            waitTime = (Random.Range(0.1f, 0.3f));
        }

        else
        {
            waitTime = parent.waitTime;
        }
        
        fractalExists = true;
        meshes  = parent.meshes;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        directionSelected = parent.directionSelected;
        spawnProbability = parent.spawnProbability;


        //The parent of transform (current frac) needs to be the same as the parents transform
        //This creates a proper heirarchy of children in Heirarchy window
        transform.parent = parent.transform;

        //The scale of the transform relative to the parent
        // Vector3.one = (1,1,1) scale
        // multiply by childScale to allow user to change size of children

        transform.localScale = Vector3.one * childScale;

        //Position of the transform relative to the parent - moves up by Vector3 'direction' (when Vector3.up is used)
        //childDirections[i] is used to indicate the current Vector3 in the array

        //Move position so that new child doesn't get hidden inside parent
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);

        transform.localRotation = childOrientations[childIndex];

       

}
	


    void OnCollisionEnter(Collision other)
    {

        
        if (other.gameObject.CompareTag("Player"))
            {
           
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<BoxCollider>());
            //Maybe emit a particle effect here

            //Destroy(gameObject, 0.2f);



            
            
           
            
            
              
             
            

            
           

            
            }

        
    }



    
}
