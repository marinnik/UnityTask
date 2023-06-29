using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    private ScreenController screenController;
    // Start is called before the first frame update
    void Start()
    {
        screenController = GameObject.Find("EventSystem").GetComponent<ScreenController>();
    }

    public void SceneChanger()
    {
        screenController.SetNameView(name);
        screenController.ChangeScenes();
    }
}
