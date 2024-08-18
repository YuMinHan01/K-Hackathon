using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

namespace LifeJacket.Buckel
{
    public class Buckel : MonoBehaviour
    {
        public bool isSelect = false;
        private LineRenderer lineRenderer;

        [SerializeField]
        private Transform startPoint, endPoint;

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
        public void SetTransform(Vector3 buckelPosition, Vector3 buckelRotation)
        {
            transform.localPosition = buckelPosition;
            transform.rotation = Quaternion.Euler(buckelRotation);
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
        public void OnFasten()
        {
            GetComponentInChildren<BoxCollider>().enabled = false;

            Vector3 newVector3 = new Vector3(0, -0.47f, 0);
            endPoint.localPosition = newVector3;
        }
    }
}