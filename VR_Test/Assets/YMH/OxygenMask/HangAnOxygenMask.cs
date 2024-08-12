using LifeJacket.Lever;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class HangAnOxygenMask : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform[] points;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = GetComponentsInChildren<Transform>();

        CreateLine();
    }

    public void CreateLine()
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = points[1].localPosition;
        linePoints[1] = points[2].localPosition;
        //if (isSelect)
        //{
        //    //실시간 선 따라가기
        //    linePoints[1] = transform.InverseTransformPoint(points[2].position);
        //}
        //else
        //{
        //    linePoints[1] = points[1].transform.localPosition;
        //}

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}
