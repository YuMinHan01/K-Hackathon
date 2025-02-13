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

    VR_FadeEffect fadeEffect;

    float initialSFXLength = 0f;

    private void Awake(){
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
        landingUI = GameObject.Find("Landing UI");
        sea = GameObject.Find("Sea");
        //fadeManager = GameObject.Find("FadeIn_FadeOut").GetComponent<FadeManager>();
        fadeEffect = GameObject.Find("FadeIn-Out").GetComponent<VR_FadeEffect>();
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
            landingUI.SetActive(true);
            StartCoroutine(LandingCoroutine());
            SoundManager.ScenEnd = false;
            return;
        }
    }

    IEnumerator LandingCoroutine(){
        soundManager.PlaySFX("비상착수시소리");
        initialSFXLength = soundManager.SFXLength("비상착수시소리");
        Debug.Log(initialSFXLength);
        yield return new WaitForSeconds(initialSFXLength + 3f);
        sea.SetActive(true);    // 바다 활성화
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        yield return fadeEffect.FadeIn(); // 화면 검게 변함
        yield return new WaitForSeconds(1.0f); // 잠깐 대기
        yield return fadeEffect.FadeOut(); // 화면 밝아짐
        Debug.Log("LandingScen_3 완료");
    }



}
