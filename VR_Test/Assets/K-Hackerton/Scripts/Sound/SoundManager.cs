using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스가 있을 경우 제거
        }
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }
        Debug.Log(p_sfxName + " 이름의 효과음이 없습니다.");
    }

    // 코루틴을 사용하여 일정 시간 후에 다음 SFX를 재생하는 기능 추가
    public void PlayBGMAndScheduleNextSFX(string bgmName, string nextSFXName, float delayAfterBGM)
    {
        StartCoroutine(PlayNextSFXAfterBGM(bgmName, nextSFXName, delayAfterBGM));
    }

    private IEnumerator PlayNextSFXAfterBGM(string bgmName, string nextSFXName, float delay)
    {
        PlayBGM(bgmName);

        // 현재 재생 중인 BGM의 길이만큼 대기
        yield return new WaitForSeconds(bgmPlayer.clip.length + delay);

        // 다음 SFX 재생
        PlaySFX(nextSFXName);
    }

    // 즉시 SFX를 재생하고 일정 시간 후에 다른 SFX를 재생하는 메서드
    public void PlaySFXWithDelay(string initialSFXName, string delayedSFXName, float delay)
    {
        StartCoroutine(PlayDelayedSFX(initialSFXName, delayedSFXName, delay));
    }

    private IEnumerator PlayDelayedSFX(string initialSFXName, string delayedSFXName, float delay)
    {
        PlaySFX(initialSFXName);

        // 첫 번째 SFX의 길이만큼 대기
        float initialSFXLength = 0f;
        foreach (Sound s in sfx)
        {
            if (s.name == initialSFXName)
            {
                initialSFXLength = s.clip.length;
                break;
            }
        }

        // 첫 번째 SFX가 끝난 후 추가적인 지연을 적용
        yield return new WaitForSeconds(initialSFXLength + delay);

        PlaySFX(delayedSFXName);
    }
}
