using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverString : MonoBehaviour
{
    [SerializeField]
    private Transform endPoint_1, endPoint_2;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateString(Vector3? endPosition)
    {
        Vector3[] linePoints = new Vector3[endPosition == null ? 2 : 3];
        linePoints[0] = endPoint_1.localPosition;
        if(endPosition != null)
        {
            linePoints[1] = transform.InverseTransformPoint(endPosition.Value);
        }
        linePoints[^1] = endPoint_2.localPosition;
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    private void Start()
    {
        CreateString(null);
    }
}
