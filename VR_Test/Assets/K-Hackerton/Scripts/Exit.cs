using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Transform destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(AgentMove());
        }
        
    }
    IEnumerator AgentMove()
    {
        agent.SetDestination(destination.position);
        yield return null;
    }
}
