using UnityEngine;
using UnityEngine.InputSystem; // Input System 사용

public class DoorController : MonoBehaviour
{
    public GameObject closeDoorTail;
    public GameObject openDoorTail;

    private void Start()
    {
        // 초기 설정 또는 필요한 경우
    }

    public void OpenDoor()
    {
        closeDoorTail.SetActive(false);
        openDoorTail.SetActive(true);
    }

    public void CloseDoor()
    {
        openDoorTail.SetActive(false);
        closeDoorTail.SetActive(true);
    }

    // Input System을 사용하여 키 입력 처리
    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame) // 'O' 키를 눌렀을 때 문 열기
        {
            OpenDoor();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame) // 'C' 키를 눌렀을 때 문 닫기
        {
            CloseDoor();
        }
    }
}
