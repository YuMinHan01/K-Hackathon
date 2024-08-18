using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeJacket.Buckel
{
    public class BuckelNumber : MonoBehaviour
    {
        public int buckelNum;
        private BuckelManager buckelManager;

        private void Start()
        {
            buckelManager = GetComponentInParent<BuckelManager>();   
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Buckel"))
            {
                Debug.Log("collision");
                buckelManager.WearBuckel();
            }
        }
    }
}