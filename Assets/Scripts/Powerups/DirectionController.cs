using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {


    // <summary>

    //  This class controls the spawning of various arrows that add directionality to the fractal bullets.
    //  The bullets start as a normal sphere, then the user must collect all 6 (up, down, left, right, forward, back) in order to upgrade their bullets
    
    GameController gameCont;
    GameObject player;
    Transform target;
    ChordPlayer chord;



    void Start ()
    {
        gameCont = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        chord = FindObjectOfType<ChordPlayer>();
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        
        
	}
	
    
	
	void Update ()
    {


        FacePlayer();
    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //RandomizeAudioClip();
            if (gameObject.CompareTag("UpArrow"))
            {
                //collect arrow and add direction
                chord.PlayChord();
                gameCont.AddDirection(0);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("RightArrow"))
            {

                chord.PlayChord();
                gameCont.AddDirection(1);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("LeftArrow"))
            {

                chord.PlayChord();
                gameCont.AddDirection(2);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("ForwardArrow"))
            {

                chord.PlayChord();
                gameCont.AddDirection(3);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("BackwardArrow"))
            {
                
                chord.PlayChord(); 
                gameCont.AddDirection(4);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("DownArrow"))
            {

                chord.PlayChord();
                gameCont.AddDirection(5);
                Destroy(gameObject);
            }
        }
    }

    void FacePlayer()
    {
        //<summary>

        //This function ensures that the arrows are always pointing towards the player, and thus display the correct directionality.

        //</summary>

        Vector3 relativePos = transform.position - target.position;
        
        

        if (gameObject.CompareTag("UpArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, target.up);
        }

        if (gameObject.CompareTag("LeftArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, -target.right);
        }

        if (gameObject.CompareTag("RightArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, target.right);
        }

        if (gameObject.CompareTag("ForwardArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, target.up);
            transform.Rotate(90, 0, 0, Space.Self);
        }

        if (gameObject.CompareTag("BackwardArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, -target.up);
            transform.Rotate(-90, 0, 0, Space.Self);
        }

        if (gameObject.CompareTag("DownArrow"))
        {
            transform.rotation = Quaternion.LookRotation(relativePos, -target.up);
        }
    }
}
