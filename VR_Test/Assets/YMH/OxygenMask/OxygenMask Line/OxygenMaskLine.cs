using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace OxygenMask.Line
{
    public class OxygenMaskLine : MonoBehaviour
    {
        private Rigidbody rigid;
        private LineRenderer lineRenderer;
        private Transform[] linePoints;
        private Vector3 midPoint;
        private XRGrabInteractable interactable;
        private bool isSelect = false;

        private void Start()
        {
            rigid = GetComponentInChildren<Rigidbody>();
            lineRenderer = GetComponent<LineRenderer>();
            linePoints = new Transform[3];

            for (int index = 0; index < 3; index++)
                linePoints[index] = transform.GetChild(index);
            interactable = linePoints[1].GetComponent<XRGrabInteractable>();

            interactable.selectEntered.AddListener(OnSelectEntered);
            interactable.selectExited.AddListener(OnSelectExited);

            midPoint = CalculateControlPoint(linePoints);
            DrawQuadraticBezierCurve(linePoints[0].position, midPoint, linePoints[2].position);
        }
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            isSelect = true;
            rigid.isKinematic = false;
        }
        private void OnSelectExited(SelectExitEventArgs args)
        {
            isSelect = false;
            rigid.isKinematic = true;
        }
        void Update()
        {
            if (!isSelect)
                return;

            midPoint = CalculateControlPoint(linePoints);
            DrawQuadraticBezierCurve(linePoints[0].position, midPoint, linePoints[2].position);
        }
        private Vector3 CalculateControlPoint(Transform[] linePoints)
        {
            Vector3 point0 = linePoints[0].position;
            Vector3 point1 = linePoints[1].position;
            Vector3 point2 = linePoints[2].position;

            return (2 * point1) - (0.5f * (point0 + point2));
        }
        void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
        {
            lineRenderer.positionCount = 200;
            float t = 0f;
            Vector3 B = Vector3.zero;
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
                lineRenderer.SetPosition(i, B);
                t += (1 / (float)lineRenderer.positionCount);
            }
        }
    }
}