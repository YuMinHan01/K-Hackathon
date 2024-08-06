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
    [Header("구명조끼 형태")]
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
    [Header("구명조끼 크기 및 소요시간")]
    [SerializeField]
    [Tooltip("구명조끼가 부풀지 않았을 경우의 크기")]
    private float minSize;
    [SerializeField]
    [Tooltip("구명조끼가 부풀었을 경우의 크기")]
    private float maxSize;
    [SerializeField]
    [Tooltip("구명조끼가 부풀어지는데 걸리는 시간")]
    private float requiredTime;
    [SerializeField]
    [Tooltip("구명조끼 레버 길이")]
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
            //목 부분에 착용
            interactable.enabled = false;
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
            //구명조끼 외형 변경
            lifeJacket.WearLifeJacket(lifeJacketMesh[1], lifeJacketMaterial[1]);
            //구명조끼 기능 활성화
            leverManager.OnLever();
            buckelManager.OnBuckel();
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