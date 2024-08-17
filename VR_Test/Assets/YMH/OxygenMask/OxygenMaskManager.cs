using OxygenMask.Line;
using OxygenMask.Wear;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Attachment;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace OxygenMask
{
    public class OxygenMaskManager : MonoBehaviour
    {
        [Header("산소 마스크 기본 설정")]
        [SerializeField]
        [Tooltip("산소 마스크 떨어질 때 산소 튜브 최대 길이")]
        private float springDistance;
        [SerializeField]
        [Tooltip("산소 마스크 쪼이는 줄 길이")]
        private float pullDistance;
        [SerializeField]
        [Tooltip("산소 마스크 커지기 전 크기")]
        private float beforePullSize;
        [SerializeField]
        [Tooltip("산소 마스크 커지기 후 크기")]
        private float afterPullSize;

        private bool isWear = false;
        private OxygenMaskLine lineScript;
        private WearOxygenMask[] wearScripts;
        private HangAnOxygenMask hangAnScript;
        private OxygenMaskPull pullScript;
        private XRGrabInteractable[] interactables;

        private Rigidbody rigid;
        private XRGrabInteractable oxygenMaskGrabInteractable;

        private void Start()
        {
            lineScript = GetComponentInChildren<OxygenMaskLine>();
            wearScripts = GetComponentsInChildren<WearOxygenMask>();
            hangAnScript = GetComponentInChildren<HangAnOxygenMask>();
            pullScript = GetComponentInChildren<OxygenMaskPull>();
            interactables = GetComponentsInChildren<XRGrabInteractable>();

            rigid = GetComponent<Rigidbody>();
            oxygenMaskGrabInteractable = GetComponent<XRGrabInteractable>();
            oxygenMaskGrabInteractable.selectEntered.AddListener(OnSelectEntered);
            oxygenMaskGrabInteractable.selectExited.AddListener(OnSelectExited);

            hangAnScript.Init(springDistance);
            pullScript.Init(pullDistance, beforePullSize, afterPullSize);
        }
        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                OnDrop();
            }
            if (Keyboard.current.pKey.wasPressedThisFrame)
                OnWear();

            if (wearScripts[0].isSameDifference && wearScripts[1].isSameDifference)
            {
                OnWear();
            }
        }
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            hangAnScript.OnSelectEnterdOxygenMask();
        }
        private void OnSelectExited(SelectExitEventArgs args)
        {
            hangAnScript.OnSelectExitedOxygenMask();
        }
        private void OnWear()
        {
            //손으로 잡고 있는 물체 모두 놓기
            foreach (XRGrabInteractable interactable in interactables)
            {
                var interactor = interactable.firstInteractorSelecting;
                if (interactor != null)
                {
                    interactable.interactionManager.SelectExit(interactor, interactable);
                }
            }
            //산소 마스크 위치 변경
            Transform NosePosition = Camera.main.transform.Find("Nose").transform;
            transform.SetParent(NosePosition);
            transform.localPosition = new Vector3(-0.0015f, -0.03f, 0.04f);
            transform.localRotation = new Quaternion(0, 180, 0, 0);

            //lineScript.DisappearLine();
            hangAnScript.OnSelectEnterdOxygenMask();
            pullScript.OnPull();
        }
        public void OnDrop() 
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
            oxygenMaskGrabInteractable.enabled = true;
            
            foreach(WearOxygenMask wearScript in wearScripts)
            {
                wearScript.OnDrop();
            }
            hangAnScript.StartCreateLine();
        }
        public void ExitDrop()
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
    }
}