using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ImageView : MonoBehaviour
{
    private Canvas canvas;
    private string url = "http://data.ikppbb.com/test-task-unity-data/pics/";
    public UnityEngine.UI.Image image;


    void Start()
    {

        canvas = FindAnyObjectByType<Canvas>();
        StartCoroutine(GetTexture());
    }

    private void Update()
    {
        image.rectTransform.localScale = new Vector3(1, 1, 1);
        image.rectTransform.sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().rect.width,
            canvas.GetComponent<RectTransform>().rect.height);
        image.rectTransform.localPosition = new Vector3(0, 0, 0);
    }
    public IEnumerator GetTexture()
    {
        UnityWebRequest loader = UnityWebRequestTexture.GetTexture(url + ScreenController.imageName);
        loader.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
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
    }

    class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
    {

        // Encoded RSAPublicKey
        private static string PUB_KEY = "mypublickey";

        protected override bool ValidateCertificate(byte[] certificateData)
        {
            X509Certificate2 certificate = new X509Certificate2(certificateData);
            string pk = certificate.GetPublicKeyString();
            if (pk.ToLower().Equals(PUB_KEY.ToLower()))
                return true;

            return false;
        }
    }
}
