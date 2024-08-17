using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OxygenMask.Wear 
{
    public enum Type
    {
        Nose,
        BackHead,
    }
    public class WearOxygenMask : MonoBehaviour
    {
        [SerializeField]
        private Type type;
        private Collider collider;
        [HideInInspector]
        public bool isSameDifference = false;

        private void Start()
        {
            collider = GetComponent<Collider>();
        }
        public void OnDrop()
        {
            StartCoroutine(Drop());
        }
        private IEnumerator Drop()
        {
            yield return new WaitForSeconds(0.5F);
            collider.isTrigger = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(type.ToString()))
            {
                Debug.Log($"{type.ToString()} 何盒 立盟");
                isSameDifference = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(type.ToString()))
            {
                Debug.Log($"{type.ToString()} 何盒 立盟 秦力");
                isSameDifference = false;
            }
        }
    }
}