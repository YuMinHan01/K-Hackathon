using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LifeJacket : MonoBehaviour
{
    private LifeJacketRotate lifeJackeRotate;
    private XRGrabInteractable interactable;
    private Lever lever;
    private Rigidbody rigid;
    private CapsuleCollider lifeJacketCollider;

    private GameObject playerNeckPosition;
    bool isPosition;

    private void Awake()
    {
        lifeJackeRotate = GetComponent<LifeJacketRotate>();
        interactable = GetComponent<XRGrabInteractable>();
        lever = GetComponentInChildren<Lever>();
        rigid = GetComponent<Rigidbody>();
        lifeJacketCollider = GetComponentInChildren<CapsuleCollider>();
    }
    private void Start()
    {
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
            playerNeckPosition = GameObject.Find("Neck Position");
            gameObject.transform.SetParent(playerNeckPosition.transform);
            gameObject.transform.localPosition = new Vector3(0, -0.3f, 0);
            gameObject.transform.localRotation = Quaternion.identity;
            rigid.isKinematic = true;
            rigid.useGravity = false;
            GetComponentInChildren<MeshCollider>().enabled = false;
            lifeJackeRotate.isWear = true;
        }
        lifeJacketCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("¸ñ¿¡ Á¢ÃË");
            isPosition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("¸ñ¿¡¼­ ¹þ¾î³²");
            isPosition = false;
        }
    }
}