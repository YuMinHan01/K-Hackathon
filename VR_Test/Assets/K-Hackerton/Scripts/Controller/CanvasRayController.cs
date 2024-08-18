using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CanvasRayController : MonoBehaviour
{
    public GameObject Description_UI;
    public GameObject SelectScenario_UI; // Canvas 오브젝트
    public XRRayInteractor rightRayInteractor; // RightController의 XR Ray Interactor

    void Start()
    {
        if (Description_UI.activeSelf || SelectScenario_UI.activeSelf)
        {
            ActivateRay();
        }
        else
        {
            DeactivateRay();
        }
    }

    void Update()
    {
        // Canvas가 활성화되면 Ray를 활성화하고, 비활성화되면 Ray도 비활성화
        if (Description_UI.activeSelf || SelectScenario_UI.activeSelf)
        {
            ActivateRay();
        }
        else
        {
            DeactivateRay();
        }
    }

    void ActivateRay()
    {
        rightRayInteractor.enabled = true; // Ray Interactor 활성화
    }

    void DeactivateRay()
    {
        rightRayInteractor.enabled = false; // Ray Interactor 비활성화
    }
}
