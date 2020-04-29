using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [SerializeField]
    GameObject fadeCanvas;

    [SerializeField]
    Animator fadeAnim;

    [SerializeField]
    Image blackImg;

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void FadeIn(string levelName)
    {
        StartCoroutine(FadeInAnimation(levelName));
    }

    void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    IEnumerator FadeInAnimation(string levelName)
    {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        Application.LoadLevel(levelName);
        FadeOut();
    }

    IEnumerator FadeOutAnimation()
    {
        fadeAnim.Play("FadeOut");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }
}
