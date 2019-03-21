using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    GameObject player;
    
    //obstaclePresent detects objects in front of enemy

    private bool obstaclePresent;
    public float enemyMoveSpeed;

    // Use this for initialization
	void Start ()
    {
        obstaclePresent = false;

		player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * enemyMoveSpeed * Time.deltaTime);
        //transform.Translate(new Vector3(0,3,5) * enemyMoveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
       if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }


}
