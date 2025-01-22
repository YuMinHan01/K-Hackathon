using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    private List<IScenario> scenarios = new List<IScenario>();
    private int currentScenarioIndex = 0;

    void Start()
    {
        // 각 시나리오를 순서대로 등록
        scenarios.Add(FindObjectOfType<TakeOffScen_1>());
        scenarios.Add(FindObjectOfType<OxygemMaskScen_2>());
        scenarios.Add(FindObjectOfType<LandingScen_3>());
        scenarios.Add(FindObjectOfType<EscapeScen_4>());

        StartNextScenario();
    }

    void Update()
    {
        if (currentScenarioIndex < scenarios.Count){
            IScenario currentScenario = scenarios[currentScenarioIndex];
            if (currentScenario.IsScenarioComplete()){
                currentScenarioIndex++;
                StartNextScenario();
            }
        }
    }

    private void StartNextScenario()
    {
        if (currentScenarioIndex < scenarios.Count)
        {
            IScenario nextScenario = scenarios[currentScenarioIndex];
            nextScenario.StartScenario();
            Debug.Log($"시나리오 {currentScenarioIndex + 1} 시작");
        }
        else
        {
            Debug.Log("모든 시나리오 완료");
        }
    }
}
