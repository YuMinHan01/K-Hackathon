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
        [HideInInspector]
        public bool isWear = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(type.ToString()))
            {
                Debug.Log($"{type.ToString()} 何盒 立盟");
                isWear = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(type.ToString()))
            {
                Debug.Log($"{type.ToString()} 何盒 立盟 秦力");
                isWear = false;
            }
        }
    }
}