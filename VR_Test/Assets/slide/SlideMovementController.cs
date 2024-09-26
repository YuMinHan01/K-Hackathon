using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SlideMovementController : MonoBehaviour
{
    [SerializeField] private GameObject handle;          // 손잡이 오브젝트
    [SerializeField] private GameObject slide;
    [SerializeField] private GameObject slidePoint;      // 슬라이드 포인트 (슬라이드의 시작 위치)
    [SerializeField] private GameObject rope;            // Rope 오브젝트
    [SerializeField] private GameObject airliner;        // Airliner 오브젝트 (이 오브젝트가 움직임)
    [SerializeField] private GameObject OxygenMask;      // OxygenMask 오브젝트

    public float triggerDistance = 1.0f;  // 일정 거리를 public 변수로 설정
    public float slideSpeed = 1.0f;       // 슬라이드의 이동 속도
    public float xIncrement = 0.1f;       // X축으로 이동하는 증가량
    public float zIncrement = 0.1f;       // Z축으로 이동하는 증가량

    private bool slideActivated = false;  // 슬라이드가 이미 활성화되었는지 확인하는 플래그

    void Update()
    {
        // slide Move
        if (slideActivated)
        {
            NavMeshDestroy(airliner);
            OxygenMaskDestroy();
            MoveAirliner();
        }
        else
        {
            // Handle과 SlidePoint 간의 거리를 계산
            float distance = Vector3.Distance(handle.transform.position, slidePoint.transform.position);

            // 거리가 설정된 triggerDistance 이상이면 로프와 핸들을 파괴하고 Airliner 이동 시작
            if (distance >= triggerDistance)
            {
                ActivateSlide();
            }
        }
    }

    private void ActivateSlide()
    {
        // Rope와 Handle 오브젝트를 파괴
        Destroy(rope);
        Destroy(handle);

        // Open_door_tail 오브젝트를 Airliner에서 분리
        slide.transform.parent = null;

        // Airliner 이동을 활성화
        slideActivated = true;
    }

    private void MoveAirliner()
    {
        // Airliner의 현재 위치를 가져와서 이동시킴
        Vector3 newPosition = airliner.transform.position;

        // X축과 Z축 방향으로 이동량을 더함
        newPosition.x += xIncrement * slideSpeed * Time.deltaTime;
        newPosition.z += zIncrement * slideSpeed * Time.deltaTime;

        // 새로운 위치로 Airliner를 이동
        airliner.transform.position = newPosition;
    }

    private void NavMeshDestroy(GameObject airliner)
    {
        // NavMeshSurface가 존재하는지 확인
        NavMeshSurface agent = airliner.GetComponent<NavMeshSurface>();
        if (agent != null)
        {
            // NavMeshSurface가 있을 경우 비활성화
            agent.enabled = false;
        }
        else
        {
            // 존재하지 않으면 경고 로그 출력
            Debug.LogWarning("NavMeshSurface 컴포넌트가 Airliner 오브젝트에 존재하지 않습니다.");
        }
    }

    private void OxygenMaskDestroy()
    {
        Destroy(OxygenMask);
    }
}
