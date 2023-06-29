using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Xml.Linq;

public class GetPicture : MonoBehaviour
{
    public Canvas canvas;
    public GameObject scrollView;
    private string url = "http://data.ikppbb.com/test-task-unity-data/pics/";
    public int picNum = 1;
    public UnityEngine.UI.Image image;
    private bool ready = true;
    public float height;
    public float yPos;
    public float canvasHeight;

    void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        StartCoroutine(SpawnImage());
    }

    private void Update()
    {
        height = scrollView.GetComponent<RectTransform>().rect.height;
        yPos = scrollView.GetComponent<RectTransform>().localPosition.y;
    }
    IEnumerator SpawnImage()
    {
        yield return new WaitForSeconds(3);
        while (picNum <= 66)
        {
            yield return new WaitUntil(() => (height - yPos <= canvasHeight));
            yield return new WaitUntil(() => ready);
            Instantiate(image, scrollView.GetComponent<Transform>());
            ready = false;
        }
    }

    public IEnumerator GetTexture()
    {
        UnityWebRequest loader = UnityWebRequestTexture.GetTexture(url + picNum + ".jpg");
        yield return loader.SendWebRequest();

        if (loader.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(loader.error);
        }
        else
        {
            Texture2D tempTex = ((DownloadHandlerTexture)loader.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(.5f, .5f));
            image.sprite = newSprite;
        }
        picNum++;
        ready = true;
    }

}
