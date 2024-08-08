using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BuckelManager : MonoBehaviour
{
    Buckel[] buckels;
    XRGrabInteractable[] interactables;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        buckels = GetComponentsInChildren<Buckel>();
        interactables = GetComponentsInChildren<XRGrabInteractable>();

        interactables[0].selectEntered.AddListener(OnSelectEntered);
        interactables[1].selectEntered.AddListener(OnSelectEntered);
        interactables[0].
        interactables[0].selectExited.AddListener(OnSelectExited);
        interactables[1].selectExited.AddListener(OnSelectExited);
    }
    public void Init()
    {
        gameObject.SetActive(false);
    }
    public void OnBuckel()
    {
        gameObject.SetActive(true);

        buckels[0].CreateString();
        buckels[1].CreateString();
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        int buckelNum = args.interactableObject.transform.GetComponentInParent<BuckelData>().buckelNum;
        args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        buckels[buckelNum - 1].OnSelectEntered();
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        int buckelNum = args.interactableObject.transform.GetComponentInParent<BuckelData>().buckelNum;
        args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        buckels[buckelNum - 1].OnSelectExited();
    }
}