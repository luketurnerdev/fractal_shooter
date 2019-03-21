using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    EnemySpawner enemySpawner;
    public Transform top;
    public Transform bottom;

    Transform lift;
    Transform ramp;
    public float moveRate;
    bool movingUp;
	
	void Start ()
    {
        lift = GameObject.FindWithTag("Lift").transform;
        ramp = GameObject.FindWithTag("Ramp").transform;

        enemySpawner = FindObjectOfType<EnemySpawner>();
        
        movingUp = true;
	}

    IEnumerator MoveUp()
    {
        if (transform.position.y <= top.position.y)
        {
            transform.Translate(0, moveRate * 0.1f, 0 * Time.deltaTime);
        }

        else
        {
            yield return new WaitForSeconds(2);
            movingUp = false;

        }
        
    }

    IEnumerator MoveDown()
    {
        if (transform.position.y >= bottom.position.y)
        {
            transform.Translate(0, -moveRate * 0.1f, 0 * Time.deltaTime);
        }

        else
        {
            yield return new WaitForSeconds(2);

            movingUp = true;
        }

       

    }

    private void OperateElevator()
    {
        if (movingUp)
        {
            StartCoroutine(MoveUp());
        }
        
        else
        {
            StartCoroutine(MoveDown());
        }
        
        
    }

    

    void Update ()
    {
        OperateElevator();
	}
}
