using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScreenController : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public float progress = 0;
    public TMP_Text loadProg;
    private string sceneName;
    static public string imageName;
    private AsyncOperation sceneAsync;

    public void ChangeScenes()
    {
        StartCoroutine(LoadScene(sceneName));
        sceneName = null;
    }

    public void SetNameGallery()
    {
        sceneName = "Gallery";
    }
    public void SetNameView(string name)
    {
        sceneName = "View";
        imageName = name;
    }
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation sceneChange = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);

        while (!sceneChange.isDone)
        {
            slider.value = sceneChange.progress;
            progress = sceneChange.progress * 100;
            loadProg.SetText(progress + "%");
            yield return null;
        }
    }
}
