using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Xml.Linq;

public class SpawnImageOnView : MonoBehaviour
{
    public Canvas canvas;
    public UnityEngine.UI.Button button;
    public UnityEngine.UI.Image image;

    void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        StartCoroutine(SpawnImage());
        StartCoroutine(SpawnButton());
    }

    IEnumerator SpawnImage()
    {
        Instantiate(image, canvas.GetComponent<Transform>());
        yield return null;
    }

    IEnumerator SpawnButton()
    {
        Instantiate(button, canvas.GetComponent<Transform>());
        yield return null;
    }

}
