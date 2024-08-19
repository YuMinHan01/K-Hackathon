using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class FlightAttendent : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Transform destination;
    //[SerializeField]
    //private float speed;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private bool exit;
    private bool escape;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        exit = false;
        escape = false;
    }
    private void OnCollisionEnter(Collision other) {
        agent.speed = 0;
        animator.SetTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if(exit)
        {
            StartCoroutine(AgentMove());
        }

        if(escape)
        {
            StartCoroutine(Escape());
        }

        //animator.SetFloat("Speed", speed);
    }
    // void FixedUpdate() {
    //     speed = agent.velocity.magnitude;
    // }

    public void Exit()
    {
        exit = true;
    }
    public void EscapeAll()
    {
        escape = true;
    }
    IEnumerator AgentMove()
    {
        Debug.Log("Exit");
        animator.SetTrigger("Exit");
        exit = false;
        yield return null;
    }
    
    IEnumerator Escape()
    {
        Debug.Log("Escape");
        animator.SetTrigger("Escape");
        agent.SetDestination(destination.position);
        escape = false;
        yield return null;
    }
}
