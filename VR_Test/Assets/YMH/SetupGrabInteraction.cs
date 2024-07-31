using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SetupGrabInteraction : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private XRDirectInteractor directInteractor;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        directInteractor = FindObjectOfType<XRDirectInteractor>();

        if (grabInteractable != null && directInteractor != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Object Grabbed: " + args.interactorObject);
        // 추가 로직
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Object Released: " + args.interactorObject);
        // 추가 로직
    }
}