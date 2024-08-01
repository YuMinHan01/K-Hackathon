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
    [Tooltip("구명조끼 레버 길이")]
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
        Debug.Log("자켓 작동");
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
        Debug.Log("자켓 기능 끄기");
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
        GUI.Box(new Rect(10, 10, 100, 90), "구명조끼 기능");

        if (GUI.Button(new Rect(20, 40, 80, 20), "구명조끼 On"))
        {
            OnUse();
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "구명조끼 Off"))
        {
            OnOff();
        }
    }
}