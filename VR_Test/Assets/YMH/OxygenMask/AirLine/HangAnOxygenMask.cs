using LifeJacket.Lever;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class HangAnOxygenMask : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] points;
    private bool isCreate = false;
    private float lineLength;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new Transform[2];
        points = GetComponentsInChildren<Transform>();
    }
    public void Init(float lineLength)
    {
        this.lineLength = lineLength;

        //lineRenderer.
    }
    public void StartCreateLine()
    {
        isCreate = true;
    }
    private void Update()
    {
        if (isCreate)
            CreateLine();
    }
    private void CreateLine()
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = points[1].localPosition;
        linePoints[1] = points[2].localPosition;

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}