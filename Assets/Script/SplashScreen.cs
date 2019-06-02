using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    public Image logo;
    public Image oti;
    public Text team1;

    IEnumerator Start() {
        logo.canvasRenderer.SetAlpha(0f);
        oti.canvasRenderer.SetAlpha(0f);
        team1.canvasRenderer.SetAlpha(0f);

        FadeIn(oti);
        yield return new WaitForSeconds(3f);
        FadeOut(oti);
        yield return new WaitForSeconds(2.5f);
        team1.CrossFadeAlpha(1f, 1.5f, false);
        yield return new WaitForSeconds(3f);
        team1.CrossFadeAlpha(0f, 1.5f, false);
        yield return new WaitForSeconds(2.5f);
        FadeIn(logo);
        yield return new WaitForSeconds(3f);
        FadeOut(logo);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    void FadeIn(Image img)
    {
        img.CrossFadeAlpha(1f, 1.5f, false);
    }

    void FadeOut(Image img)
    {
        img.CrossFadeAlpha(0f, 1.5f, false);
    }
}
