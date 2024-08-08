using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace LifeJacket.Lever 
{
    public class LeverManager : MonoBehaviour
    {
        private Lever[] levers;
        private XRGrabInteractable[] interactables;
        private float leverLength;
        private int leverState = 0;

        private void OnEnable()
        {
            levers = GetComponentsInChildren<Lever>();
            interactables = GetComponentsInChildren<XRGrabInteractable>();

            interactables[0].selectEntered.AddListener(OnSelectEntered);
            interactables[1].selectEntered.AddListener(OnSelectEntered);
            interactables[0].selectExited.AddListener(OnSelectExited);
            interactables[1].selectExited.AddListener(OnSelectExited);
        }
        public void Init(float leverLength)
        {
            this.leverLength = leverLength;
            gameObject.SetActive(false);
        }
        public void OnLever()
        {
            gameObject.SetActive(true);

            levers[0].CreateString();
            levers[1].CreateString();
        }
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            int leverNum = args.interactableObject.transform.GetComponent<LeverNumber>().leverNum;
            args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            levers[leverNum - 1].OnSelectEntered(leverLength);
        }
        private void OnSelectExited(SelectExitEventArgs args)
        {
            int leverNum = args.interactableObject.transform.GetComponent<LeverNumber>().leverNum;
            args.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            levers[leverNum - 1].OnSelectExited();

            if (leverState >= 2)
            {
                gameObject.SetActive(false);
            }
        }
        public void UseLever()
        {
            leverState += 1;
            if (leverState < 2)
                return;

            GameObject.Find("LifeJacket").GetComponent<LifeJacket.Body.LifeJacket>().UseJacket.Invoke();
        }
        private void Update()
        {
            if (levers[0].isSelect)
            {
                levers[0].CreateString();
            }
            if (levers[1].isSelect)
            {
                levers[1].CreateString();
            }
        }
    }
}