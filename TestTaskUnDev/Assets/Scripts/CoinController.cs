using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameManager gameManager;
    private float turnSpeed = 35f;
    public bool movingUp = true;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
        if (transform.position.y >= 0.75f)
        {
            movingUp = false;
        }
        if (transform.position.y <= 0.35f)
        {
            movingUp = true;
        }
        if (movingUp)
        {
            transform.position += new Vector3(0f, Time.deltaTime, 0f);
        }
        else { transform.position += new Vector3(0f, -Time.deltaTime, 0f); }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        Destroy(gameObject);
        gameManager.UpdateScore(1);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-6, 2), 0.4f, Random.Range(-100, 125));
    }
}
