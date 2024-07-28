using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private SpringJoint springJoint;
    private Camera mainCamera;
    private bool isDragging;

    private void Start()
    {
        springJoint = GetComponent<SpringJoint>();
        mainCamera = Camera.main;
        isDragging = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 hitPoint = ray.GetPoint(10);
            springJoint.connectedAnchor = hitPoint;
        }
    }
}
