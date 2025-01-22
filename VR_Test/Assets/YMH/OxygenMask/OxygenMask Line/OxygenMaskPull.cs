using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OxygenMaskPull : MonoBehaviour
{
    [SerializeField]
    private OxygemMaskScen_2 oxygenMaskScen_2;
    [SerializeField]
    private GameObject OxygenMask2;
    private PullLine[] pullLines;

    private float pullDistance;
    private float beforePullSize;
    private float afterPullSize;

    public bool isActive = false;
    public bool isLineDisabled = false; // 상태 공유 변수
    private float requiredTime = 1.0f;
    private float currentValue;

    private void Start(){
        pullLines = GetComponentsInChildren<PullLine>();
        // oxygenMaskScen_2 동적 연결
        if (oxygenMaskScen_2 == null)
        {
            oxygenMaskScen_2 = FindObjectOfType<OxygemMaskScen_2>();
            Debug.Log("OxygemMaskScen_2 동적 연결");
            if (oxygenMaskScen_2 == null)
            {
                Debug.LogError("OxygemMaskScen_2를 찾을 수 없습니다. Inspector에서 연결하세요.");
            }
        }
    }
    public void Init(float pullDistance, float beforePullSize, float afterPullSize){
        this.pullDistance = pullDistance;
        this.beforePullSize = beforePullSize;
        this.afterPullSize = afterPullSize;

        foreach (var pullLine in pullLines)
            pullLine.Init(pullDistance);

        OxygenMask2 = transform.parent.parent.Find("OxygenMask2").gameObject;
        OxygenMask2.transform.localScale = new Vector3(beforePullSize, beforePullSize, beforePullSize);
    }
    private void Update()
    {
        if (isActive && !pullLines[0].isSelect && !pullLines[1].isSelect)
        {
            gameObject.SetActive(false);
            isLineDisabled = true; // 상태 변수 업데이트
            Debug.Log("OxygenMaskLine 비활성화");
            oxygenMaskScen_2.SetisCompleted(); // 상태 변수 업데이트
            return;
        }
            

        if (pullLines[0].isActive && pullLines[1].isActive && !isActive)
        {
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
