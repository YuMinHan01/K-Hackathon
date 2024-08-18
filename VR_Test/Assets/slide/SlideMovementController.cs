using UnityEngine;

public class SlideMovementController : MonoBehaviour
{
    public GameObject handle;          // ������ ������Ʈ
    public GameObject handle_Point;
    public GameObject slidePoint;      // �����̵� ����Ʈ (�����̵��� ���� ��ġ)
    public GameObject rope;            // Rope ������Ʈ
    public GameObject airliner;        // Airliner ������Ʈ (�� ������Ʈ�� ������)
    public GameObject openDoorTail;    // Open_door_tail ������Ʈ (�и��� ������Ʈ)

    public float triggerDistance = 1.0f;  // ���� �Ÿ��� public ������ ����
    public float slideSpeed = 1.0f;       // �����̵��� �̵� �ӵ�
    public float xIncrement = 0.1f;       // X������ �̵��ϴ� ������
    public float zIncrement = 0.1f;       // Z������ �̵��ϴ� ������

    private bool slideActivated = false;  // �����̵尡 �̹� Ȱ��ȭ�Ǿ����� Ȯ���ϴ� �÷���

    void Update()
    {
        if (slideActivated)
        {
            MoveAirliner();
        }
        else
        {
            // Handle�� SlidePoint ���� �Ÿ��� ���
            float distance = Vector3.Distance(handle_Point.transform.position, slidePoint.transform.position);

            // �Ÿ��� ������ triggerDistance �̻��̸� ������ �ڵ��� �ı��ϰ� Airliner �̵� ����
            if (distance >= triggerDistance)
            {
                ActivateSlide();
            }
        }
    }

    private void ActivateSlide()
    {
        // Rope�� Handle ������Ʈ�� �ı�
        Destroy(rope);
        Destroy(handle);

        // Open_door_tail ������Ʈ�� Airliner���� �и�
        openDoorTail.transform.parent = null;

        // Airliner �̵��� Ȱ��ȭ
        slideActivated = true;
    }

    private void MoveAirliner()
    {
        // Airliner�� ���� ��ġ�� �����ͼ� �̵���Ŵ
        Vector3 newPosition = airliner.transform.position;

        // X��� Z�� �������� �̵����� ����
        newPosition.x += xIncrement * slideSpeed * Time.deltaTime;
        newPosition.z += zIncrement * slideSpeed * Time.deltaTime;

        // ���ο� ��ġ�� Airliner�� �̵�
        airliner.transform.position = newPosition;
    }
}
