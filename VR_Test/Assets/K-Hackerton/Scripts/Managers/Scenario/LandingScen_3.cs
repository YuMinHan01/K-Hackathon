using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScen_3 : MonoBehaviour, IScenario
{
    private bool isCompleted = false;
    SoundManager soundManager;
    GameObject oxygenMaskUI;
    GameObject landingUI;
    GameObject sea;
    FadeManager fadeManager;

    private void Awake(){
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
        landingUI = GameObject.Find("LandingUI");
        sea = GameObject.Find("Sea");
        fadeManager = GameObject.Find("FadeIn_FadeOut").GetComponent<FadeManager>();
    }

    public void StartScenario(){
        Debug.Log("LandingScen_3 시작");
        isCompleted = false;
        InitializeScene();
        PlayLandingSequence();
    }

    public bool IsScenarioComplete(){
        return isCompleted;
    }

    private void InitializeScene(){
        oxygenMaskUI.SetActive(false);  // 산소마스크 UI 비활성화
    }

    private void PlayLandingSequence(){
        soundManager = SoundManager.instance;
        if(soundManager == null){
            Debug.LogError("SoundManager 인스턴스 설정 안됨.");
        }
        soundManager.PlaySFXWithUIDelay("비상착수안내_TTS", "비상착수직전안내_TTS", 1f, landingUI);
    }

    void Update()
    {
        if(SoundManager.ScenEnd){
            Debug.Log("ScenEnd");
            landingUI.SetActive(false);
            sea.SetActive(true);    // 바다 활성화
            StartCoroutine(LandingCoroutine());
            return;
        }
    }

    IEnumerator LandingCoroutine(){
        soundManager.PlaySFX("비상착수시소리");

        yield return new WaitForSeconds(13f);
        fadeManager.StartFade();
    }



}
