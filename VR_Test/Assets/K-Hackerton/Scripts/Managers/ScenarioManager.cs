using OxygenMask;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oxygenMaskManagers = GameObject.Find("OxygenMasks").GetComponentsInChildren<OxygenMaskManager>();
        oxygenMaskUI = GameObject.Find("OxygenMask UI");
        landingUI = GameObject.Find("Landing UI");
        water = GameObject.Find("Water");
        fadeManager = GameObject.Find("FadeIn_FadeOut").GetComponent<FadeManager>();
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
        water.SetActive(false);
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
}

