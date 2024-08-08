using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Buckel : MonoBehaviour
{
    private bool isSelect = false;
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform startPoint ,endPoint;
  
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
    }
    public void OnSelectEntered()
    {
        isSelect = true;
    }
    public void OnSelectExited()
    {
        isSelect = false;
    }
    public void CreateString()
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = startPoint.localPosition;
        if (isSelect)
        { 
            linePoints[1] = transform.InverseTransformPoint(endPoint.position);
        }
        else
        {
            linePoints[1] = endPoint.localPosition;
        }

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
    private void LateUpdate()
    {
        if (isSelect)
        {
            CreateString();
        }
    }
}