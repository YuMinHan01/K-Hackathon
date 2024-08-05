using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverString : MonoBehaviour
{
    [SerializeField]
    private Transform endPoint_1, endPoint_2;

    Lever lever;
    private LineRenderer lineRenderer;
    bool isUsed = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lever = GameObject.Find("LifeJacket").GetComponent<Lever>();
    }

    public void CreateString(Vector3? endPosition)
    {
        Vector3[] linePoints = new Vector3[2];

        linePoints[0] = endPoint_1.localPosition;
        if (endPosition != null)
        {
            //실시간 선 따라가기
            linePoints[1] = transform.InverseTransformPoint(endPosition.Value);

            //길이 측정
            float length = Vector3.Distance(linePoints[0], linePoints[1]);
            if(length >= lever.leverLength && !isUsed)
            {
                lever.UseJacket.Invoke();
                isUsed = true;
            }
        }
        else
        {
            linePoints[1] = endPoint_2.transform.localPosition;
        }

        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    private void OnEnable()
    {
        CreateString(null);
    }
}