using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public enum State{
        Slide_Sitting,
        Running,
        Sitting_Idle
    }
    [SerializeField]
    private State _state;
    private NavMeshAgent agent;
    [SerializeField]
    private Transform destination;
    //[SerializeField]
    //private float speed;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private bool runningNPC;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        _state = State.Sitting_Idle;
        runningNPC = false;
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("SlideSit");
        animator.SetTrigger("SlideSit");
        _state = State.Slide_Sitting;
    }

    public void Running()
    {
        runningNPC = true;
        Debug.Log("running");
    }

    // Update is called once per frame
    void Update()
    {
        if(runningNPC && _state == State.Sitting_Idle)
        {
            StartCoroutine(AgentMove());
        }

        //animator.SetFloat("Speed", speed);
    }
    // void FixedUpdate() {
    //     speed = agent.velocity.magnitude;
    // }

    IEnumerator AgentMove()
    {
        Debug.Log("Exit");
        animator.SetTrigger("Exit");
        agent.SetDestination(destination.position);
        _state = State.Running;
        runningNPC = false;
        yield return null;
    }
}
