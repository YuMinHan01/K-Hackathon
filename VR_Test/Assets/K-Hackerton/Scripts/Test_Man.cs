using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Test_Man : MonoBehaviour
{
    public enum State
    {
        Standing_Idle,
        Sitting_Idle,
        Running,
        Terrified

    }
    
    [SerializeField]
    private State state;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Standing_Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        switch (state)
        {
            case State.Standing_Idle:
            if(Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("Sit");
                state = State.Sitting_Idle;
            }
            //UpdateStandIdle();
            break;
            case State.Sitting_Idle:
            UpdateSitIdle();
            break;
            case State.Running:
            //UpdateRunning();
            break;
            case State.Terrified:
            break;
        }
    }

    // void UpdateRunning()
    // {
    //     Animator animator = GetComponent<Animator>();
    //     animator.SetFloat("speed", 1);
    // }

    // void UpdateStandIdle()
    // {
    //     Animator animator = GetComponent<Animator>();
    //     animator.SetFloat("speed", 0);
    // }

    void UpdateSitIdle()
    {
        Animator animator = GetComponent<Animator>();
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Struck");
        }


        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Stand");
            state = State.Standing_Idle;
        }
    }
}
