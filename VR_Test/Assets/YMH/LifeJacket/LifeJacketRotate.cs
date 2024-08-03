using UnityEngine;

public class LifeJacketRotate : MonoBehaviour
{
    [HideInInspector]
    public bool isWear = false;
    private Transform cameraTransform;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        if (!isWear)
            return;

        Quaternion parentRotation = cameraTransform.rotation;
        parentRotation.x = 0;
        parentRotation.z = 0;

        transform.rotation = parentRotation * initialLocalRotation;
        transform.localPosition = initialLocalPosition;
    }
}
