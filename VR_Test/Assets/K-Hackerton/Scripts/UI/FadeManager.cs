using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // Fade 효과를 위한 이미지
    public float fadeDuration = 1f; // Fade 효과의 지속 시간
    public Canvas fadeCanvas; // Fade 효과를 위한 Canvas



    private void Awake()
    {
        // fadeCanvas를 MainCamera의 자식으로 설정
        fadeCanvas.transform.SetParent(Camera.main.transform);
        // 카메라 바로 앞에 위치하도록 설정
        fadeCanvas.transform.localPosition = new Vector3(0f, 0f, 0.5f); // z값은 필요에 따라 조정
        fadeCanvas.transform.localRotation = Quaternion.identity;
        fadeCanvas.transform.localScale = new Vector3(1f, 1f, 1f); // 필요에 따라 조정
    }

    public void StartFade()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
    private IEnumerator FadeOut()
    {
        // Fade Out
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
}
