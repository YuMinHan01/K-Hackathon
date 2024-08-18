using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // Fade 효과를 위한 이미지
    public float fadeDuration = 1f; // Fade 효과의 지속 시간

    //private void Start()
    //{
    //    StartCoroutine(FadeIn());
    //}

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeInOnly()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }


    private IEnumerator FadeOutAndLoadScene(string sceneName)
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

        // Scene 전환 후 Fade In
        yield return SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FadeInOnly());
    }
}
