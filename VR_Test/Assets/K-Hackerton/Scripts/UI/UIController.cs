using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject descriptionUI; // Description_UI Canvas
    public GameObject selectScenarioUI; // SelectScenario_UI Canvas

    public void OnDescriptionButtonClick()
    {
        // Description_UI Canvas 비활성화
        descriptionUI.SetActive(false);

        // SelectScenario_UI Canvas 활성화
        selectScenarioUI.SetActive(true);
    }
}
