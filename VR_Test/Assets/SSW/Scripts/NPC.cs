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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        _state = State.Sitting_Idle;
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("SlideSit");
        animator.SetTrigger("SlideSit");
        _state = State.Slide_Sitting;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.zKey.wasPressedThisFrame && _state == State.Sitting_Idle)
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
        yield return null;
    }
}
