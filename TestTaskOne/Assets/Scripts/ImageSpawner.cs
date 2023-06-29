using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageSpawner : MonoBehaviour
{
    private Canvas canvas;
    private GetPicture getPicture;
    public UnityEngine.UI.Image image;
    private Vector3 exPos;
    private float width;
    private float height;
    private float cWidth;
    private float xPos;
    private float yPos;

    void Start()
    {
        
        canvas = FindAnyObjectByType<Canvas>();
        getPicture = GameObject.Find("EventSystem").GetComponent<GetPicture>();
        image.name = (getPicture.picNum - 1) + ".jpg";

        image.rectTransform.localScale = new Vector3(1, 1, 1);

        cWidth = canvas.GetComponent<RectTransform>().rect.width;

        width = cWidth / 2 - 75f;

        if (width > 500f)
        {
            width = 500f;
        }

        height = width;

        xPos = 50f + width/2;
        yPos = -height / 2 - 50f;

        image.rectTransform.sizeDelta = new Vector2(width, height);
        exPos = new Vector3(xPos, yPos, 0);

        image.rectTransform.localPosition = SpawnPos(exPos);
        StartCoroutine(getPicture.GetTexture());
    }

    Vector3 SpawnPos(Vector3 exPos)
    {
        float x;
        int row = (getPicture.picNum + 1) / 2;
        if (getPicture.picNum % 2 == 0) { x = xPos; row++; }
        else { x = cWidth - xPos; }
        Debug.Log(row);
        float y = height + 50f;
        
        return new Vector3(x, exPos.y - y * (row - 2), 0);
    }

}
