using System.Collections;
using System.Collections.Generic;
using OxygenMask;
using UnityEngine;

public class OxygemMaskScen_2 : MonoBehaviour, IScenario
{
    [SerializeField]
    private OxygenMaskPull oxygenMaskPull; // OxygenMaskPull 참조
    private bool isCompleted = false;
    SoundManager soundManager;
    OxygenMaskManager[] oxygenMaskManagers;
    GameObject oxygenMaskUI;

    private void Awake(){
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskManagers = GameObject.Find("OxygenMasks").GetComponentsInChildren<OxygenMaskManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
    }

    private void Update(){
        // OxygenMaskPull 상태 확인
        if (oxygenMaskPull != null && oxygenMaskPull.isLineDisabled){
            isCompleted = true;
            Debug.Log("OxygemMaskScen_2 완료");
        }
    }
    public void StartScenario(){
        Debug.Log("OxygemMaskScen_2 시작");
        isCompleted = false;
        InitializeScene();
        PlayOxygenMaskSequence();
    }

    public bool IsScenarioComplete(){
        return isCompleted;
    }

    private void InitializeScene(){
        oxygenMaskUI.SetActive(false);  // 산소마스크 UI 비활성화
    }

    private void PlayOxygenMaskSequence(){
        soundManager = SoundManager.instance;
        if (soundManager == null){
            Debug.LogError("SoundManager 인스턴스 설정 안됨.");
        }
        foreach(OxygenMaskManager oxygenMaskManager in oxygenMaskManagers){
            oxygenMaskManager.OnDrop(); // 산소마스크 떨어뜨리기
        }
        StartCoroutine(OxygenMaskInfoAudio(1f));    // 산소마스크 안내 음성 재생 및 산소마스크 UI 활성화
    }
    IEnumerator OxygenMaskInfoAudio(float delay){
        yield return new WaitForSeconds(delay);
        soundManager.PlaySFX("산소마스크안내_TTS");
        yield return new WaitForSeconds(delay);
        oxygenMaskUI.SetActive(true);   // 산소마스크 UI 활성화
    }

    public void SetisCompleted(){
        isCompleted = true;
        Debug.Log("OxygemMaskScen_2 완료");
    }
}
