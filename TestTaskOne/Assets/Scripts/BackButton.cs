using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void ChangeScenes()
    {
        StartCoroutine(LoadScene("Gallery"));
    }
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation sceneChange = SceneManager.LoadSceneAsync(sceneName);
        yield return null;
    }
}
