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
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Collections;

//public class FadeManager : MonoBehaviour
//{
//    public GameObject fadeQuad; // Quad GameObject
//    public Material fadeMaterial; // Quad에 적용된 Material
//    public float fadeDuration = 1f; // 페이드 효과의 지속 시간

//    private void Start()
//    {
//        fadeQuad.SetActive(false); // 초기 상태에서 Quad를 비활성화
//    }

//    public void LoadSceneWithFade(string sceneName)
//    {
//        StartCoroutine(FadeOutAndLoadScene(sceneName));
//    }

//    private IEnumerator FadeIn()
//    {
//        float elapsedTime = 0f;
//        fadeQuad.SetActive(true); // Quad 활성화

//        Color color = fadeMaterial.color;
//        color.a = 1f; // 시작 시 완전히 불투명하게 설정
//        fadeMaterial.color = color;

//        while (elapsedTime < fadeDuration)
//        {
//            elapsedTime += Time.deltaTime;
//            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // 알파 값 감소
//            color.a = alpha;
//            fadeMaterial.color = color;
//            yield return null;
//        }

//        fadeQuad.SetActive(false); // Fade In이 완료되면 Quad 비활성화
//    }

//    private IEnumerator FadeOutAndLoadScene(string sceneName)
//    {
//        float elapsedTime = 0f;
//        fadeQuad.SetActive(true); // Scene 전환 전 Quad 활성화

//        Color color = fadeMaterial.color;
//        color.a = 0f; // 시작 시 완전히 투명하게 설정
//        fadeMaterial.color = color;

//        while (elapsedTime < fadeDuration)
//        {
//            elapsedTime += Time.deltaTime;
//            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // 알파 값 증가
//            color.a = alpha;
//            fadeMaterial.color = color;
//            yield return null;
//        }

//        yield return SceneManager.LoadSceneAsync(sceneName);
//        StartCoroutine(FadeIn());
//    }
//}

