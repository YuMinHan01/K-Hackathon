using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverManager : MonoBehaviour
{
    private Lever[] lever;
    private XRGrabInteractable[] interactables;
    private float leverLength;
    private bool[] isSelect = { false, false };
    private int leverState = 0;

    private void OnEnable()
    {
        lever = GetComponentsInChildren<Lever>();
        interactables = GetComponentsInChildren<XRGrabInteractable>();

        interactables[0].selectEntered.AddListener(OnSelectEntered);
        interactables[1].selectEntered.AddListener(OnSelectEntered);
        interactables[0].selectExited.AddListener(OnSelectExited);
        interactables[1].selectExited.AddListener(OnSelectExited);

        lever[0].CreateString();
        lever[1].CreateString();
    }
    public void Init(float leverLength)
    {
        this.leverLength = leverLength;
        gameObject.SetActive(false);
    }
    public void OnLever()
    {
        gameObject.SetActive(true);
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        int leverNum = args.interactableObject.transform.GetComponentInParent<LeverData>().leverNum;
        args.interactorObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        lever[leverNum - 1].OnSelectEntered(leverLength);
        isSelect[leverNum - 1] = true;
    }
    private void OnSelectExited(SelectExitEventArgs args)
    {
        int leverNum = args.interactableObject.transform.GetComponentInParent<LeverData>().leverNum;
        args.interactorObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        lever[leverNum - 1].OnSelectExited();
        isSelect[leverNum - 1] = false;

        if(leverState >= 2)
        {
            lever[0].gameObject.SetActive(false);
            lever[1].gameObject.SetActive(false);
        }
    }
    public void UseLever()
    {
        leverState += 1;
        if (leverState < 2)
            return;

        GameObject.Find("LifeJacket").GetComponent<LifeJacket>().UseJacket.Invoke();
    }
    private void Update()
    {
        if (isSelect[0])
        {
            lever[0].CreateString();
        }
        if (isSelect[1])
        {
            lever[1].CreateString();
        }
    }
}
