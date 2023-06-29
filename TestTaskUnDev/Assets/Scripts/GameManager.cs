using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject coin;
    private int score;
    public bool isActive;
    private float spawnRate = 15.0f;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        score = 0;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnTarget()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(coin);
        }
    }
}
