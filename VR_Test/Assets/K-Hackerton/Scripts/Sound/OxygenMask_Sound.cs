using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMask_Sound : MonoBehaviour
{
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        // SoundManager의 인스턴스를 가져옵니다.
        soundManager = SoundManager.instance;

        // 첫 번째 사운드 "이륙"을 재생하고, 해당 사운드가 끝난 후 1초 지연 후 "엔진터지는소리"를 재생합니다.
        soundManager.PlaySFXWithDelay("이륙", "엔진터지는소리", 1f);
    }
}
