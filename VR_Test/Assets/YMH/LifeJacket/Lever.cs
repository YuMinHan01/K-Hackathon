using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private LeverString leverStringRenderer;
    private XRGrabInteractable interacable;

    [SerializeField]
    private Transform leverGrabObject;
    private Transform interactor;

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

    private void Awake()
    {
        interacable = leverGrabObject.GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        interacable.selectEntered.AddListener(PrepareLeverString);
        interacable.selectExited.AddListener(ResetLeverString);
        UseJacket.AddListener(OnUse);
    }

    private void ResetLeverString(SelectExitEventArgs arg0)
    {
        interactor = null;
        //leverGrabObject.localPosition = Vector3.zero;
        //leverStringRenderer.CreateString(null);
    }
    private void PrepareLeverString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }
    private void Update()
    {
        if(interactor != null)
        {
            leverStringRenderer.CreateString(leverGrabObject.transform.position);
        }
    }
    private void OnUse()
    {
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