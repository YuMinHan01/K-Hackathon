using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private LeverString[] leverStringRenderer = new LeverString[2];
    [SerializeField]
    private XRGrabInteractable[] interacable = new XRGrabInteractable[2];

    [SerializeField]
    private Transform[] leverGrabObject = new Transform[2];
    private Transform[] interactor = new Transform[2] { null, null };

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
    [Tooltip("�������� ���� ����")]
    public float leverLength;

    [HideInInspector]
    public UnityEvent UseJacket;
    float currentValue;
    //bool[] isGrab = new bool[2] { };
    int leverState = 0;

    private void Awake()
    {
        leverStringRenderer[0] = GameObject.Find("Lever(Left)").GetComponent<LeverString>();
        leverStringRenderer[1] = GameObject.Find("Lever(Right)").GetComponent<LeverString>();

        leverGrabObject[0] = GameObject.Find("End Point(Left)").GetComponent<Transform>();
        leverGrabObject[1] = GameObject.Find("End Point(Right)").GetComponent<Transform>();

        interacable[0] = leverGrabObject[0].GetComponent<XRGrabInteractable>();
        interacable[1] = leverGrabObject[1].GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        interacable[0].selectEntered.AddListener(PrepareLeverString);
        interacable[1].selectEntered.AddListener(PrepareLeverString);
        interacable[0].selectExited.AddListener(ResetLeverString);
        interacable[1].selectExited.AddListener(ResetLeverString);
        UseJacket.AddListener(OnUse);
    }

    private void ResetLeverString(SelectExitEventArgs arg0)
    {
        int leverNum = arg0.interactableObject.transform.GetComponent<LeverData>().leverNum;
        interactor[leverNum] = null;
    }
    private void PrepareLeverString(SelectEnterEventArgs arg0)
    {
        int leverNum = arg0.interactableObject.transform.GetComponent<LeverData>().leverNum;
        Debug.Log(leverNum);
        interactor[leverNum] = arg0.interactorObject.transform;
    }
    private void Update()
    {
        if(interactor[0] != null)
        {
            leverStringRenderer[0].CreateString(leverGrabObject[0].transform.position);
        }
        if (interactor[1] != null)
        {
            leverStringRenderer[1].CreateString(leverGrabObject[1].transform.position);
        }
    }
    private void OnUse()
    {
        leverState += 1;
        if (leverState < 2)
            return;

        Debug.Log("���� �۵�");
        StartCoroutine("TestCoroutine");
    }
    IEnumerator TestCoroutine()
    {
        float elaposedTime = 0f;

        while(elaposedTime < requiredTime)
        {
            currentValue = Mathf.Lerp(minSize, maxSize, elaposedTime / requiredTime);
            transform.localScale = new Vector3(maxSize, currentValue, maxSize);
            elaposedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnOff()
    {
        Debug.Log("���� ��� ����");
        StartCoroutine("TestOffCoroutine");
    }
    IEnumerator TestOffCoroutine()
    {
        float elaposedTime = 0f;

        while (elaposedTime < requiredTime)
        {
            currentValue = Mathf.Lerp(maxSize, minSize, elaposedTime / requiredTime);
            transform.localScale = new Vector3(maxSize, currentValue, maxSize);
            elaposedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "�������� ���");

        if (GUI.Button(new Rect(20, 40, 80, 20), "�������� On"))
        {
            OnUse();
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "�������� Off"))
        {
            OnOff();
        }
    }
}