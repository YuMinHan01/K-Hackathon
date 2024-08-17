using UnityEngine;

public class LifeJacketRotate : MonoBehaviour
{
    private bool isWear = false;
    private Transform mainCameraTransform;
    private Vector3 fixedLocalPosition = new Vector3(0, -0.3f, 0);

    private Vector3 fixedPosition;


    void OnEnable()
    {
        isWear = true;
        mainCameraTransform = Camera.main.transform;
        fixedPosition = mainCameraTransform.position + mainCameraTransform.rotation * fixedLocalPosition;
    }

    void LateUpdate()
    {
        if (!isWear)
            return;

        transform.localRotation = Quaternion.Euler(mainCameraTransform.eulerAngles.x * (-1), 0, 0);

        Vector3 fixedPosition = mainCameraTransform.position + fixedLocalPosition;
        Vector3 rotatedOffset = mainCameraTransform.rotation * new Vector3(fixedLocalPosition.x, 0, 0);
        fixedPosition.x += rotatedOffset.x;
        transform.position = fixedPosition;
    }
}