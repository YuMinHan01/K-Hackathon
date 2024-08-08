using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace LifeJacket
{
    public struct LifeJacketData
    {
        public float minSize;
        public float maxSize;
        public float requiredTime;
    }
    public struct BuckelData
    {
        public Vector3[] buckelStartPosition;
        public Vector3[] buckelStartRotation;
        public Vector3 buckelFastenPosition;
        public Vector3 buckelFastenRotation;
    }

    public class LifeJacketManager : MonoBehaviour
    {
        [Header("�������� ����")]
        [SerializeField]
        [Tooltip("�������� �޽�")]
        [NamedArray(new string[] { "Not Wear LifeJacket", "Wear LifeJacket" })]
        private List<Mesh> lifeJacketMesh;
        [SerializeField]
        [Tooltip("�������� ���׸���")]
        [NamedArray(new string[] { "Not Wear LifeJacket", "Wear LifeJacket" })]
        private List<Material> lifeJacketMaterial;

        private LifeJacketRotate lifeJacketRotate;
        private LifeJacket.Body.LifeJacket lifeJacket;
        private LifeJacket.Lever.LeverManager leverManager;
        private LifeJacket.Buckel.BuckelManager buckelManager;

        private XRGrabInteractable interactable;
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

        private BuckelData buckelData;
        [Header("��Ŭ ������")]
        [SerializeField]
        [Tooltip("��Ŭ ���� ��ġ")]
        [NamedArray(new string[] { "buckel1", "buckel2" })]
        private Vector3[] buckelStartPosition;
        [SerializeField]
        [Tooltip("��Ŭ ���� ȸ��")]
        [NamedArray(new string[] { "buckel1", "buckel2" })]
        private Vector3[] buckelStartRotation;

        [Space(10)]
        [SerializeField]
        [Tooltip("��Ŭ ���� �� ��ġ")]
        private Vector3 buckelFastenPosition;
        [SerializeField]
        [Tooltip("��Ŭ ���� �� ȸ��")]
        private Vector3 buckelFastenRotation;

        private GameObject playerNeckPosition;
        bool isPosition;

        private void Awake()
        {
            lifeJacketRotate = GetComponent<LifeJacketRotate>();
            interactable = GetComponent<XRGrabInteractable>();
            lifeJacket = GetComponentInChildren<LifeJacket.Body.LifeJacket>();
            leverManager = GetComponentInChildren<LifeJacket.Lever.LeverManager>();
            buckelManager = GetComponentInChildren<LifeJacket.Buckel.BuckelManager>();
            rigid = GetComponent<Rigidbody>();
            lifeJacketCollider = GetComponentInChildren<CapsuleCollider>();
        }
        private void Start()
        {
            lifeJacketData.minSize = minSize;
            lifeJacketData.maxSize = maxSize;
            lifeJacketData.requiredTime = requiredTime;

            buckelData.buckelStartPosition = buckelStartPosition;
            buckelData.buckelStartRotation = buckelStartRotation;
            buckelData.buckelFastenPosition = buckelFastenPosition;
            buckelData.buckelFastenRotation = buckelFastenRotation;

            lifeJacket.Init(lifeJacketMesh[0], lifeJacketMaterial[0], lifeJacketData);
            leverManager.Init(leverLength);
            buckelManager.Init(buckelData);

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
}