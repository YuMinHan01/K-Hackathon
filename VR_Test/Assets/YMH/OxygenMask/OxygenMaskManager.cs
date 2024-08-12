using OxygenMask.Line;
using OxygenMask.Wear;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Attachment;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace OxygenMask
{
    public class OxygenMaskManager : MonoBehaviour
    {
        private bool isWear = false;
        private OxygenMaskLine lineScript;
        private WearOxygenMask[] wearScripts;
        private XRGrabInteractable[] interactables;

        private void Start()
        {
            lineScript = GetComponentInChildren<OxygenMaskLine>();
            wearScripts = GetComponentsInChildren<WearOxygenMask>();
            interactables = GetComponentsInChildren<XRGrabInteractable>();
        }
        private void Update()
        {
            if (wearScripts[0].isWear && wearScripts[1].isWear)
            {
                Wear();
            }
        }
        private void Wear()
        {
            Debug.Log("산소 마스크 착용");
            foreach (XRGrabInteractable interactable in interactables)
            {
                var interactor = interactable.firstInteractorSelecting;
                if (interactor != null)
                {
                    interactable.interactionManager.SelectExit(interactor, interactable);
                }
            }
            isWear = true;
        }
    }
}