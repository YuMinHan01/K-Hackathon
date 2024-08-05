using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LifeJacket : MonoBehaviour
{
    private LifeJacketRotate lifeJacketRotate;
    private XRGrabInteractable interactable;
    private Lever lever;
    private BuckelManager buckelManager; 
    private Rigidbody rigid;
    private CapsuleCollider lifeJacketCollider;

    private GameObject playerNeckPosition;
    bool isPosition;

    private void Awake()
    {
        lifeJacketRotate = GetComponent<LifeJacketRotate>();
        interactable = GetComponent<XRGrabInteractable>();
        lever = GetComponentInChildren<Lever>();
        rigid = GetComponent<Rigidbody>();
        lifeJacketCollider = GetComponentInChildren<CapsuleCollider>();
    }
    private void Start()
    {
        lifeJacketRotate.enabled = false;
        isPosition = false;
        lifeJacketCollider.enabled = false;
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        lifeJacketCollider.enabled = true;
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (isPosition) 
        {
            //목 부분에 착용
            playerNeckPosition = GameObject.Find("Neck Position");
            gameObject.transform.SetParent(playerNeckPosition.transform);
            gameObject.transform.localPosition = new Vector3(0, -0.3f, 0);
            gameObject.transform.localRotation = Quaternion.identity;
            //구명조끼 물리 기능 제거
            rigid.isKinematic = true;
            rigid.useGravity = false;
            GetComponentInChildren<MeshCollider>().enabled = false;
            //구명조끼 회전 기능 활성화
            lifeJacketRotate.enabled = true;
            //레버 기능 활성화
            lever.OnLever();
        }
        lifeJacketCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("목에 접촉");
            isPosition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("목에서 벗어남");
            isPosition = false;
        }
    }
}