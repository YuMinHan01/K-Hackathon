using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // 흔들리게 할 카메라의 Transform

    [Header("Camera Shake Settings")]
    [Tooltip("카메라가 흔들리는 지속 시간")]
    public float shakeDuration = 1.0f; // 흔들리는 시간

    [Tooltip("카메라 흔들림의 강도")]
    public float shakeMagnitude = 0.1f; // 흔들림의 강도

    [Tooltip("카메라 흔들림이 서서히 멈추는 속도")]
    public float dampingSpeed = 1.0f; // 흔들림이 서서히 멈추는 속도

    private Vector3 initialPosition; // 카메라의 초기 위치
    private float currentShakeDuration = 0f; // 현재 남아있는 흔들림 시간

    private float noiseSeedX;
    private float noiseSeedY;
    private float noiseSeedZ;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }

        initialPosition = cameraTransform.localPosition;

        // Perlin Noise의 시드를 생성합니다.
        noiseSeedX = Random.Range(0f, 10f);
        noiseSeedY = Random.Range(0f, 10f);
        noiseSeedZ = Random.Range(0f, 10f);
    }

    void Update()
    {
        // 키보드의 b 키를 눌렀을 때 흔들림 시작
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log("카메라 흔들림");
            TriggerShake(shakeDuration);
        }

        if (currentShakeDuration > 0)
        {
            // Perlin Noise를 이용한 흔들림 구현
            float x = Mathf.PerlinNoise(noiseSeedX, Time.time * shakeMagnitude) * 2 - 1;
            float y = Mathf.PerlinNoise(noiseSeedY, Time.time * shakeMagnitude) * 2 - 1;
            float z = Mathf.PerlinNoise(noiseSeedZ, Time.time * shakeMagnitude) * 2 - 1;

            cameraTransform.localPosition = initialPosition + new Vector3(x, y, z);

            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    // 외부에서 호출하여 카메라를 흔들리게 하는 메서드
    public void TriggerShake(float duration)
    {
        currentShakeDuration = duration;
    }
}
