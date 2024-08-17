using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Transform destination;
    [SerializeField]
    private float speed;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(AgentMove());
        }
        animator.SetFloat("Speed", speed);
        
    }

    void FixedUpdate() {
        speed = agent.velocity.magnitude;
    }
    IEnumerator AgentMove()
    {
        agent.SetDestination(destination.position);
        yield return null;
    }
}
