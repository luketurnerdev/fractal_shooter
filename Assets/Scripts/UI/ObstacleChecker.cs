using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Excellent, there's an obstacle.");
            gameObject.transform.Rotate(0, 90, 0);
        }
    }
}
