using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffScen_1 : MonoBehaviour, IScenario
{
    private bool isCompleted = false;
    SoundManager soundManager;
    GameObject oxygenMaskUI;
    GameObject landingUI;
    GameObject lifeJacketUI;
    GameObject EndingUI;
    GameObject sea;
    GameObject runningNPC;
    GameObject flightAttendent;

    private void Awake(){
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
        landingUI = GameObject.Find("Landing UI");
        lifeJacketUI = GameObject.Find("LifeJacket UI");
        EndingUI = GameObject.Find("Ending UI");
        sea = GameObject.Find("Sea");
        runningNPC = GameObject.Find("Running NPC");
        flightAttendent = GameObject.Find("Flight Attendent");
    }

    public void StartScenario(){
        Debug.Log("TakeOffScen_1 시작");
        isCompleted = false;
        InitializeScene();
        PlayTakeOffeSequence();
    }

    public bool IsScenarioComplete(){
        return isCompleted;
    }

    private void InitializeScene(){
        oxygenMaskUI.SetActive(false);  // 산소마스크 UI 비활성화
        landingUI.SetActive(false); // 착륙 UI 비활성화
        lifeJacketUI.SetActive(false);  // 구명조끼 UI 비활성화
        EndingUI.SetActive(false);  // 엔딩 UI 비활성화
        sea.SetActive(false); // 바다 비활성화
        runningNPC.SetActive(false);    // NPC 비활성화
        flightAttendent.SetActive(false);   // 승무원 비활성화
    }

    private void PlayTakeOffeSequence(){
        soundManager = SoundManager.instance;
        if (soundManager == null){
            Debug.LogError("SoundManager 인스턴스 설정 안됨.");
        }
        soundManager.PlaySFXWithDoubleDelay("비행기이륙", "비행기엔진터지는소리", 1f, "비상상황발생_TTS", 2f, totalTime => {
            StartCoroutine(CompleteScenario(totalTime));
        });
    }

    IEnumerator CompleteScenario(float totalTime){
        yield return new WaitForSeconds(totalTime);
        isCompleted = true;
        Debug.Log("TakeOffScen_1 완료");
    }

}
