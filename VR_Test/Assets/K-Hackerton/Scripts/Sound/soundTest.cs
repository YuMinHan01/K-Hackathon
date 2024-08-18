using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class soundTest : MonoBehaviour
{
    SoundManager theAudio;


    // Start is called before the first frame update
    void Start()
    {
        theAudio = SoundManager.instance;

        if (theAudio == null)
        {
            Debug.LogError("SoundManager instance is not set. Ensure that SoundManager is present in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (theAudio != null)
            {
                theAudio.PlaySFX("엔진터지는소리");
            }
            else
            {
                Debug.LogError("The SoundManager instance is null.");
            }
        }
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            if (theAudio != null)
            {
                theAudio.PlaySFX("이륙");
            }
            else
            {
                Debug.LogError("The SoundManager instance is null.");
            }
        }
    }
}
