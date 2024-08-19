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
        public Vector3 poision;

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

            endPoint.localPosition = poision;
            endPoint.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));

            CreateString();
        }
    }
}