using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace LifeJacket.Body 
{
    public class LifeJacket : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private float minSize;
        private float maxSize;
        private float requiredTime;
        private float leverLength;

        [HideInInspector]
        public UnityEvent UseJacket;
        float currentValue;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            UseJacket.AddListener(OnUse);
        }
        public void Init(Mesh lifeJacketMesh, Material lifeJacketMaterial, LifeJacketData data)
        {
            meshFilter.mesh = lifeJacketMesh;
            meshRenderer.material = lifeJacketMaterial;

            minSize = data.minSize;
            maxSize = data.maxSize;
            requiredTime = data.requiredTime;

            transform.localScale = new Vector3(maxSize, minSize, maxSize);
        }
        public void WearLifeJacket(Mesh lifeJacketMesh, Material lifeJacketMaterial)
        {
            meshFilter.mesh = lifeJacketMesh;
            meshRenderer.material = lifeJacketMaterial;
        }
        private void OnUse()
        {
            StartCoroutine("UseCoroutine");
        }
        IEnumerator UseCoroutine()
        {
            float elaposedTime = 0f;

            while (elaposedTime < requiredTime)
            {
                currentValue = Mathf.Lerp(minSize, maxSize, elaposedTime / requiredTime);
                transform.localScale = new Vector3(maxSize, currentValue, maxSize);
                elaposedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}