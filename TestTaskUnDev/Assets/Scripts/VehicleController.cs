using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    private PlayerInput playerInput;
    public GameObject player;
    private GameManager gameManager;
    public bool inVehicle;
    private float speed = 8.5f;
    private float turnSpeed = 25.0f;
    private float rotationSpeed = 1.0f;
    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        inVehicle = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerInput = GetComponent<PlayerInput>();
        startPos = transform.position; 
        startRot = transform.rotation;
        //vehicleManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inVehicle)
        {
            Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
            Vector2 lookInput = playerInput.actions["Look"].ReadValue<Vector2>();

            transform.Translate(Vector3.forward * Time.deltaTime * speed * moveInput.y);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * moveInput.x);

            if (playerInput.actions["Action"].triggered) { 
                inVehicle = false;
                player.transform.position = PlayerSpawnPos();
                player.gameObject.SetActive(true);
            }
        }

        if (transform.position.y < -3f)
        {
            transform.position = startPos;
            transform.rotation = startRot;
            gameManager.isActive = false;
        }
    }

    public Vector3 PlayerSpawnPos()
    {
        return new Vector3(transform.position.x - 1.5f, 0.006f, transform.position.z);
    }
}
