using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private LeverString leverStringRenderer;
    private XRGrabInteractable interacable;

    [SerializeField]
    private Transform leverGrabObject;
    private Transform interactor;

    private void Awake()
    {
        interacable = leverGrabObject.GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        interacable.selectEntered.AddListener(PrepareLeverString);
        interacable.selectExited.AddListener(ResetLeverString);
    }

    private void ResetLeverString(SelectExitEventArgs arg0)
    {
        interactor = null;
        leverGrabObject.localPosition = Vector3.zero;
        leverStringRenderer.CreateString(null);
    }
    private void PrepareLeverString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }

    private void Update()
    {
        if(interactor != null)
        {
            leverStringRenderer.CreateString(leverGrabObject.transform.position);
        }
    }
}
