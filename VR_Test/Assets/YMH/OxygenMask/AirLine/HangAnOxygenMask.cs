using LifeJacket.Lever;
using OxygenMask;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class HangAnOxygenMask : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] points;
    private float springDistance;
    private bool isCreate = false;
    private bool isSelect = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new Transform[2];
        points = GetComponentsInChildren<Transform>();
    }
    public void Init(float springDistance)
    {
        this.springDistance = springDistance;
    }
    public void StartCreateLine()
    {
        isCreate = true;
    }
    public void OnSelectEnterdOxygenMask()
    {
       isSelect = true;
    }
    public void OnSelectExitedOxygenMask()
    {
        isSelect = false;
    }
    private void Update()
    {
        if (isCreate)
        {
            CreateLine();

            float length = Vector3.Distance(points[1].position, points[2].position);
            if (length >= springDistance)
            {
                isCreate = false;
                GetComponentInParent<OxygenMaskManager>().ExitDrop();
            }
        }
        else if(isSelect)
        {
            CreateLine();
        }
    }
    private void CreateLine()
    {
        //줄 그리기
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = points[1].localPosition;
        linePoints[1] = points[2].localPosition;

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}