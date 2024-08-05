using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Buckel : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform startPoint, buckelPoint ,endPoint;

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startPoint = transform.GetChild(0);
        buckelPoint = transform.GetChild(1); 
        endPoint = buckelPoint.GetChild(0);
    }

    public void CreateString(Vector3? endPosition)
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = startPoint.localPosition;
        if (endPosition != null)
        {
            //실시간 선 따라가기
            linePoints[1] = transform.InverseTransformPoint(endPosition.Value + buckelPoint.position);
        }
        else
        {
            linePoints[1] = endPoint.transform.localPosition + buckelPoint.localPosition * ;
        }

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
}