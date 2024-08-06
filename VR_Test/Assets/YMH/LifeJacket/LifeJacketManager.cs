using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public struct LifeJacketData
{
    public float minSize;
    public float maxSize;
    public float requiredTime;
}

public class LifeJacketManager : MonoBehaviour
{
    [Header("�������� ����")]
    [SerializeField]
    private List<Mesh> lifeJacketMesh;
    [SerializeField]
    private List<Material> lifeJacketMaterial;

    private LifeJacketRotate lifeJacketRotate;
    private XRGrabInteractable interactable;
    private LifeJacket lifeJacket;
    private LeverManager leverManager;
    private BuckelManager buckelManager; 
    private Rigidbody rigid;
    private CapsuleCollider lifeJacketCollider;

    
    private LifeJacketData lifeJacketData;
    [Header("�������� ũ�� �� �ҿ�ð�")]
    [SerializeField]
    [Tooltip("���������� ��Ǯ�� �ʾ��� ����� ũ��")]
    private float minSize;
    [SerializeField]
    [Tooltip("���������� ��Ǯ���� ����� ũ��")]
    private float maxSize;
    [SerializeField]
    [Tooltip("���������� ��Ǯ�����µ� �ɸ��� �ð�")]
    private float requiredTime;
    [SerializeField]
    [Tooltip("�������� ���� ����")]
    private float leverLength;

    private GameObject playerNeckPosition;
    bool isPosition;

    private void Awake()
    {
        lifeJacketRotate = GetComponent<LifeJacketRotate>();
        interactable = GetComponent<XRGrabInteractable>();
        lifeJacket = GetComponentInChildren<LifeJacket>();
        leverManager = GetComponentInChildren<LeverManager>();
        buckelManager = GetComponentInChildren<BuckelManager>();
        rigid = GetComponent<Rigidbody>();
        lifeJacketCollider = GetComponentInChildren<CapsuleCollider>();
    }
    private void Start()
    {
        lifeJacketData.minSize = minSize;
        lifeJacketData.maxSize = maxSize;
        lifeJacketData.requiredTime = requiredTime;
        lifeJacket.Init(lifeJacketMesh[0], lifeJacketMaterial[0], lifeJacketData);
        leverManager.Init(leverLength);
        buckelManager.Init();

        lifeJacketRotate.enabled = false;
        lifeJacketCollider.enabled = false;
        isPosition = false;

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
            //�� �κп� ����
            interactable.enabled = false;
            playerNeckPosition = GameObject.Find("Neck Position");
            gameObject.transform.SetParent(playerNeckPosition.transform);
            gameObject.transform.localPosition = new Vector3(0, -0.3f, 0);
            gameObject.transform.localRotation = Quaternion.identity;
            //�������� ���� ��� ����
            rigid.isKinematic = true;
            rigid.useGravity = false;
            GetComponentInChildren<MeshCollider>().enabled = false;
            //�������� ȸ�� ��� Ȱ��ȭ
            lifeJacketRotate.enabled = true;
            //�������� ���� ����
            lifeJacket.WearLifeJacket(lifeJacketMesh[1], lifeJacketMaterial[1]);
            //�������� ��� Ȱ��ȭ
            leverManager.OnLever();
            buckelManager.OnBuckel();
        }
        lifeJacketCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("�� ����");
            isPosition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JacketPosition"))
        {
            Debug.Log("�񿡼� ���");
            isPosition = false;
        }
    }
}