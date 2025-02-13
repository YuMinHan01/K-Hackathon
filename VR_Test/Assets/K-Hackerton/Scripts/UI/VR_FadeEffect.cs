using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VR_FadeEffect : MonoBehaviour
{
    public Image fadeImage;  // 페이드 효과를 줄 이미지 (검정 화면)
    public float fadeDuration = 1.0f; // 페이드 지속 시간

    private void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image가 설정되지 않았습니다.");
            return;
        }

        StartCoroutine(FadeOut());
    }

    // 페이드 인 (투명 → 검정)
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 0; // 처음에는 투명

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 점점 어두워짐
            fadeImage.color = fadeColor;
            yield return null;
        }
    }

    // 페이드 아웃 (검정 → 투명)
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 1; // 처음에는 검정색

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration)); // 점점 밝아짐
            fadeImage.color = fadeColor;
            yield return null;
        }
    }

    // 페이드 인 & 아웃을 트리거하는 함수
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
