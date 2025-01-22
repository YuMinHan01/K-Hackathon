using UnityEngine;

public class OxygenMaskController : MonoBehaviour
{
    [SerializeField]
    private GameObject oxygenMaskLine; // OxygenMask Line 오브젝트

    private bool isLineDisabled = false; // Line 비활성화 상태 확인 플래그

    public delegate void MaskEventHandler(); // 이벤트 핸들러
    public static event MaskEventHandler OnMaskLineDisabled; // 이벤트 선언

    private void Update()
    {
        // OxygenMask Line이 비활성화되었는지 확인
        if (!isLineDisabled && oxygenMaskLine != null && !oxygenMaskLine.activeSelf)
        {
            isLineDisabled = true; // 상태 업데이트
            Debug.Log("OxygenMask Line 비활성화 확인");

            // 이벤트 호출
            OnMaskLineDisabled?.Invoke();
        }
    }
}
