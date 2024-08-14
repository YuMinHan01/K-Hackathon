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
    private float springForce;
    private float springDamper;
    private float springMass;
    private float springDistance;
    private bool isCreate = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new Transform[2];
        points = GetComponentsInChildren<Transform>();
    }
    public void Init(SpringData springData)
    {
        springForce = springData.springForce;
        springDamper = springData.springDamper;
        springMass = springData.springMass;
        springDistance = springData.springDistance;
    }
    public void StartCreateLine()
    {
        isCreate = true;
    }
    private void Update()
    {
        if (isCreate)
        {
            CreateLine();

            float length = Vector3.Distance(points[1].localPosition, points[2].localPosition);
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