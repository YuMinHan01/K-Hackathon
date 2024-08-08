using UnityEngine;

namespace LifeJacket.Lever
{
    public class Lever : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private float leverLength;
        [HideInInspector]
        public bool isSelect = false;
        private bool isUsed = false;
        private Transform startPoint, endPoint;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            startPoint = transform.GetChild(0);
            endPoint = transform.GetChild(1);
        }
        private void OnEnable()
        {
            CreateString();
        }
        public void CreateString()
        {
            Vector3[] linePoints = new Vector3[2];

            linePoints[0] = startPoint.localPosition;
            if (isSelect)
            {
                //실시간 선 따라가기
                linePoints[1] = transform.InverseTransformPoint(endPoint.position);

                //길이 측정
                float length = Vector3.Distance(linePoints[0], linePoints[1]);
                if (length >= leverLength && !isUsed)
                {
                    Debug.Log("구명조끼 사용");
                    GetComponentInParent<LeverManager>().UseLever();
                    isUsed = true;
                }
            }
            else
            {
                linePoints[1] = endPoint.transform.localPosition;
            }

            lineRenderer.positionCount = linePoints.Length;
            lineRenderer.SetPositions(linePoints);
        }
        public void OnSelectEntered(float leverLength)
        {
            this.leverLength = leverLength;
            isSelect = true;
        }
        public void OnSelectExited()
        {
            isSelect = false;
        }
    }
}