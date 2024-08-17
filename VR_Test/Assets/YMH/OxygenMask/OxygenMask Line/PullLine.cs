using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PullLine : MonoBehaviour
{
    private Transform[] points;
    private LineRenderer lineRenderer;
    private XRGrabInteractable pullObject;
    private Rigidbody rigid;
    
    private bool isSelect = false;
    private float pullDistance;
    private float beforePullSize;
    private float afterPullSize;

    private void Start()
    {
        points = GetComponentsInChildren<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
        pullObject = GetComponentInChildren<XRGrabInteractable>();
        rigid = GetComponentInChildren<Rigidbody>();

        pullObject.selectEntered.AddListener(OnSelectEntered);
        pullObject.selectExited.AddListener(OnSelectExited);

        points[1].gameObject.SetActive(false);
    }
    public void Init(float pullDistance, float beforePullSize, float afterPullSize)
    {
        this.pullDistance = pullDistance;
        this.beforePullSize = beforePullSize;
        this.afterPullSize = afterPullSize;
    }
    private void Update()
    {
        if (isSelect)
            DrawLine();
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        rigid.isKinematic = false;
        isSelect = true;
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        rigid.isKinematic = true;
        isSelect = false;
    }
    public void OnPull()
    {
        points[1].gameObject.SetActive(true);

        lineRenderer.enabled = true;
        DrawLine();
    }
    private void DrawLine()
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = Vector3.zero;
        if (isSelect)
        {
            linePoints[1] = transform.InverseTransformPoint(points[1].position);

            //당기면 부풀는 기능
            //if()
        }
        else
        {
            linePoints[1] = points[1].localPosition;
        }

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}
