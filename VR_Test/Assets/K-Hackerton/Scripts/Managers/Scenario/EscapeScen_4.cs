using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeScen_4 : MonoBehaviour, IScenario
{
    private bool isCompleted = false;
    SoundManager soundManager;
    GameObject runningNPC;
    GameObject FlightAttendent;
    FlightAttendent flightAttendentScripts;
    GameObject lifeJacketUI;
    GameObject EndingUI;
    NPC[] runningNPCScripts;

    private void Awake(){
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        runningNPC = GameObject.Find("Running NPC");
        runningNPCScripts = runningNPC.GetComponentsInChildren<NPC>();
        FlightAttendent = GameObject.Find("Flight Attendent");
        flightAttendentScripts = GameObject.Find("Flight Attendent").GetComponent<FlightAttendent>();
        lifeJacketUI = GameObject.Find("LifeJacket UI");
        EndingUI = GameObject.Find("Ending UI");
    }

    public void StartScenario(){
        Debug.Log("EscapeScen_4 시작");
        isCompleted = false;
        PlayEscapeSequence();
    }

    public bool IsScenarioComplete(){
        return isCompleted;
    }

    private void PlayEscapeSequence(){
        soundManager = SoundManager.instance;
        if (soundManager == null){
            Debug.LogError("SoundManager 인스턴스 설정 안됨.");
        }
        isCompleted = false;
        StartCoroutine(RunningCoroutine());
    }

    IEnumerator RunningCoroutine(){
        runningNPC.SetActive(true);
        FlightAttendent.SetActive(true);
        foreach(NPC npc in runningNPCScripts){
            npc.Running();
        }
        flightAttendentScripts.Exit();  // 승무원까지 탈출인지 아직 모름
        soundManager.PlaySFX("비상탈출안내_TTS");

        yield return new WaitForSeconds(1f);
        
        lifeJacketUI.SetActive(true);
    }

}
