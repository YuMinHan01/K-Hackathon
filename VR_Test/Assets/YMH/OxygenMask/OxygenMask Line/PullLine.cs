using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PullLine : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    private LineRenderer lineRenderer;
    private XRGrabInteractable pullObject;

    private void Start()
    {
        points = GetComponentsInChildren<Transform>();
        points[1].gameObject.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        pullObject = GetComponentInChildren<XRGrabInteractable>();

        pullObject.selectEntered.AddListener(OnSelectEntered);
        pullObject.selectExited.AddListener(OnSelectExited);
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {

    }
    public void OnPull()
    {
        points[1].gameObject.SetActive(true);

        lineRenderer.enabled = true;
        DrawLine();
    }
    private void DrawLine()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, points[1].localPosition);
    }
}
