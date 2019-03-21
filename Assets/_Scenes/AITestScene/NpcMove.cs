using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour {

    
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;

	void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        

        if (target == null)
        {
            Debug.Log("No sphere found");
        }

        

        if (navMeshAgent = null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }

        else
        {
            SetDestination();
        }
	}
	
    private void SetDestination()
    {
        if (target != null)
        {
            Vector3 targetVector = target.transform.position;
            Debug.Log(targetVector);
            navMeshAgent.SetDestination(targetVector);
            
        }
    }

	
}
