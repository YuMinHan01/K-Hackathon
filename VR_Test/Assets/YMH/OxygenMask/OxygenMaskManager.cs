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
        [Header("줄 길이")]
        [SerializeField]
        [Tooltip("산소 마스크 떨어질 때 산소 튜브 최대 길이")]
        private float hangAnDistance;

        private bool isWear = false;
        private OxygenMaskLine lineScript;
        private WearOxygenMask[] wearScripts;
        private HangAnOxygenMask hangAnScript;
        private XRGrabInteractable[] interactables;

        private Rigidbody rigid;
        private XRGrabInteractable oxygenMaskGrabInteractable;

        private void Start()
        {
            lineScript = GetComponentInChildren<OxygenMaskLine>();
            wearScripts = GetComponentsInChildren<WearOxygenMask>();
            hangAnScript = GetComponentInChildren<HangAnOxygenMask>();
            interactables = GetComponentsInChildren<XRGrabInteractable>();

            rigid = GetComponent<Rigidbody>();
            oxygenMaskGrabInteractable = GetComponent<XRGrabInteractable>();

            hangAnScript.Init(hangAnDistance);
        }
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                OnDrop();
            }

            if (wearScripts[0].isSameDifference && wearScripts[1].isSameDifference)
            {
                OnWear();
            }
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
            Transform NosePosition = Camera.main.transform.Find("Nose");
            transform.SetParent(NosePosition);
            transform.localPosition = new Vector3(-0.0015f, -0.03f, 0.04f);
            transform.rotation = new Quaternion(0, 180, 0, 0);

            lineScript.DisappearLine();

            isWear = true;
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
    }
}