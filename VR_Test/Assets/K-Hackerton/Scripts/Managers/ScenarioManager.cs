using LifeJacket.Body;
using OxygenMask;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScenarioManager : MonoBehaviour
{
    SoundManager soundManager;
    OxygenMaskManager[] oxygenMaskManagers;
    GameObject oxygenMaskUI;
    GameObject landingUI;
    GameObject water;
    FadeManager fadeManager;
    GameObject runningNPC;
    GameObject flightAttendent;
    FlightAttendent flightAttendentScripts;
    GameObject lifeJacketUI;
    GameObject EndingUI;

    NPC[] runningNPCScripts;

    private void Awake()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskManagers = GameObject.Find("OxygenMasks").GetComponentsInChildren<OxygenMaskManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
        landingUI = GameObject.Find("Landing UI");
        water = GameObject.Find("Water");
        fadeManager = GameObject.Find("FadeIn_FadeOut").GetComponent<FadeManager>();
        runningNPC = GameObject.Find("Running NPC");
        runningNPCScripts = runningNPC.GetComponentsInChildren<NPC>();
        flightAttendent = GameObject.Find("Flight Attendent");
        flightAttendentScripts = GameObject.Find("Flight Attendent").GetComponent<FlightAttendent>();
        lifeJacketUI = GameObject.Find("LifeJacket UI");
        EndingUI = GameObject.Find("Ending UI");
    }
    void Start()
    {
        soundManager = SoundManager.instance;
        if (soundManager == null)
        {
            Debug.LogError("SoundManager instance is not set. Ensure that SoundManager is present in the scene.");
        }
        oxygenMaskUI.SetActive(false);
        landingUI.SetActive(false);
        lifeJacketUI.SetActive(false);
        EndingUI.SetActive(false);
        water.SetActive(false);
        runningNPC.SetActive(false);
        flightAttendent.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.numpad1Key.wasPressedThisFrame)
        { 
            soundManager.PlaySFXWithDoubleDelay("이륙", "엔진터지는소리", 1f, "TTS_1", 2f);
        }
        if (Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            foreach(OxygenMaskManager oxygemMaskManager in oxygenMaskManagers)
            {
                oxygemMaskManager.OnDrop();
            }

            StartCoroutine(OxygemMaskInfoAudio(1f));
        }
        if (Keyboard.current.numpad3Key.wasPressedThisFrame)
        {
            oxygenMaskUI.SetActive(false);
            soundManager.PlaySFXWithUIDelay("TTS_3", "TTS_5", 1f, landingUI);
        }
        if (Keyboard.current.numpad4Key.wasPressedThisFrame)
        {
            landingUI.SetActive(false);
            water.SetActive(true);
            StartCoroutine(LandingCoroutine());
        }
        if (Keyboard.current.numpad5Key.wasPressedThisFrame)
        {
            StartCoroutine(RunningCoroutine());
        }
        if (Keyboard.current.numpad6Key.wasPressedThisFrame)
        {
            flightAttendentScripts.EscapeAll();
        }
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            EndingUI.SetActive(true);
        }
    }

    private IEnumerator OxygemMaskInfoAudio(float delay)
    {
        yield return new WaitForSeconds(delay);
        soundManager.PlaySFX("TTS_2");
        yield return new WaitForSeconds(delay);
        oxygenMaskUI.SetActive(true);
    }
    private IEnumerator LandingCoroutine()
    {
        water.SetActive(true);
        soundManager.PlaySFX("도착시착수사운드");

        yield return new WaitForSeconds(12f);

        fadeManager.StartFade();
    }
    private IEnumerator RunningCoroutine()
    {
        runningNPC.SetActive(true);
        flightAttendent.SetActive(true);
        foreach (NPC npc in runningNPCScripts)
        {
            npc.Running();
        }
        flightAttendentScripts.Exit();

        soundManager.PlaySFX("TTS_6");

        yield return new WaitForSeconds(1f);

        lifeJacketUI.SetActive(true);
    }
}

