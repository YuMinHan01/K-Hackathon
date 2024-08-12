using LifeJacket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace LifeJacket.Buckel
{
    public class BuckelManager : MonoBehaviour
    {
        private Buckel[] buckels;
        private XRGrabInteractable[] interactables;

        private Vector3[] buckelStartPosition;
        private Vector3[] buckelStartRotation;
        private Vector3 buckelFastenPosition;
        private Vector3 buckelFastenRotation;

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
            interactables[0].selectExited.AddListener(OnSelectExited);
            interactables[1].selectExited.AddListener(OnSelectExited);
        }
        public void Init(BuckelData buckelData)
        {
            buckelStartPosition = buckelData.buckelStartPosition;
            buckelStartRotation = buckelData.buckelStartRotation;
            buckelFastenPosition = buckelData.buckelFastenPosition;
            buckelFastenRotation = buckelData.buckelFastenRotation;

            gameObject.SetActive(false);
        }
        public void OnBuckel()
        {
            gameObject.SetActive(true);

            buckels[0].SetTransform(buckelStartPosition[0], buckelStartRotation[0]);
            buckels[1].SetTransform(buckelStartPosition[1], buckelStartRotation[1]);

            buckels[0].CreateString();
            buckels[1].CreateString();
        }
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            int buckelNum = args.interactableObject.transform.GetComponentInParent<BuckelNumber>().buckelNum;
            args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            buckels[buckelNum - 1].OnSelectEntered();
        }
        private void OnSelectExited(SelectExitEventArgs args)
        {
            int buckelNum = args.interactableObject.transform.GetComponentInParent<BuckelNumber>().buckelNum;
            args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            buckels[buckelNum - 1].OnSelectExited();
        }
        public void WearBuckel()
        {
            if (buckels[0].isSelect && buckels[1].isSelect)
            {
                Debug.Log("¹öÅ¬ Âø¿ë");

                for(int i = 0; i < 2; i++)
                {
                    var interactor = interactables[i].firstInteractorSelecting;
                    interactables[i].interactionManager.SelectExit(interactor, interactables[i]);
                    interactables[i].enabled = false;

                    buckels[i].OnFasten();
                }
            }
        }
    }
}