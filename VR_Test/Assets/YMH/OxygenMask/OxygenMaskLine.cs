using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMaskLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform[] linePoints;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        linePoints = new Transform[3];

        for(int index = 0; index < 3; index++)
            linePoints[index] = transform.GetChild(index);
    }
    void Update()
    {
        DrawQuadraticBezierCurve(linePoints[0].position, linePoints[1].position, linePoints[2].position);
    }

    void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = 200;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }
}
