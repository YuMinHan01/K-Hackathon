using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMaskPull : MonoBehaviour
{
    [SerializeField]
    private GameObject OxygenMask2;
    private PullLine[] pullLines;

    private float pullDistance;
    private float beforePullSize;
    private float afterPullSize;

    private bool isActive = false;
    private float requiredTime = 1.0f;
    private float currentValue;

    private void Start()
    {
        pullLines = GetComponentsInChildren<PullLine>();
    }
    public void Init(float pullDistance, float beforePullSize, float afterPullSize)
    {
        this.pullDistance = pullDistance;
        this.beforePullSize = beforePullSize;
        this.afterPullSize = afterPullSize;

        foreach (var pullLine in pullLines)
            pullLine.Init(pullDistance);

        OxygenMask2 = GameObject.Find("OxygenMask2");
        OxygenMask2.transform.localScale = new Vector3(beforePullSize, beforePullSize, beforePullSize);
    }
    private void Update()
    {
        if (isActive && !pullLines[0].isSelect && !pullLines[1].isSelect)
        {
            gameObject.SetActive(false);
            return;
        }
            

        if (pullLines[0].isActive && pullLines[1].isActive)
        {
            Debug.Log("魂家付胶农 何前");
            isActive = true;
            OnInflates();
        }
    }
    private void OnInflates()
    {
        StartCoroutine(InflatesCoroutine());
    }
    private IEnumerator InflatesCoroutine()
    {
        float elaposedTime = 0f;

        while (elaposedTime < requiredTime)
        {
            currentValue = Mathf.Lerp(beforePullSize, afterPullSize, elaposedTime / requiredTime);
            OxygenMask2.transform.localScale = new Vector3(beforePullSize, beforePullSize, currentValue);
            elaposedTime += Time.deltaTime;
            yield return null;
        }
    }
    public void OnPull()
    {
        pullLines[0].OnPull();
        pullLines[1].OnPull();
    }
}
