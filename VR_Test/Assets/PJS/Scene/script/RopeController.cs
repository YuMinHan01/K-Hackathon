using UnityEngine;

public class RopeController : MonoBehaviour
{
    public GameObject handle; // 손잡이 오브젝트
    public GameObject slide;  // 슬라이드 오브젝트
    private LineRenderer lineRenderer;

    void Start()
    {
        // Rope 오브젝트에 있는 Line Renderer 컴포넌트를 가져옵니다.
        lineRenderer = GetComponent<LineRenderer>();

        // Line Renderer의 포인트 수를 2개로 설정합니다.
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Line Renderer의 첫 번째 포인트를 Handle 위치로 설정
        lineRenderer.SetPosition(0, handle.transform.position); // 시작점

        // Line Renderer의 두 번째 포인트를 Slide 위치로 설정
        lineRenderer.SetPosition(1, slide.transform.position); // 끝점
    }
}
