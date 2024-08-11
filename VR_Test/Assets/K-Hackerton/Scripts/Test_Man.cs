using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private State _state;

    [SerializeField]
    private float speed;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // rb.useGravity = true;
        
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        _state = State.Standing_Idle;
    }

    // Update is called once per frame
    void Update()
    {
    
        switch (_state)
        {
            case State.Standing_Idle:
            if(Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(HandleSitAnimation());
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Struck");
        }


        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Stand");
            StartCoroutine(HandleStandAnimation());
            _state = State.Standing_Idle;
        }
    }

    public void SetStateSittingIdle()
    {
        _state = State.Sitting_Idle;
    }

    void OnKeyboard()
    {

    }

    IEnumerator HandleSitAnimation()
    {
        animator.SetTrigger("Sit");
        rb.isKinematic = true;
        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            yield return null;
        }
        AdjustColliderForSitting(capsuleCollider);
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        rb.isKinematic = false;

        //ResetColliderAfterSitting(capsuleCollider);

        yield return new WaitForEndOfFrame();
        // yield return null;
        _state = State.Sitting_Idle;
    }
    IEnumerator HandleStandAnimation()
    {
        animator.SetTrigger("Stand");
        rb.isKinematic = true;
        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        ResetCollider(capsuleCollider);
        rb.isKinematic = false;
        yield return new WaitForEndOfFrame();
        _state = State.Standing_Idle;
    }

    void AdjustColliderForSitting(CapsuleCollider collider)
    {
        collider.height = 1.0f;
        collider.center = new Vector3(0, 0.9f, 0);
    }

    void ResetCollider(CapsuleCollider collider)
    {
        collider.height = 2.0f;
        collider.center = new Vector3(0, 1.0f, 0);
    }
}
