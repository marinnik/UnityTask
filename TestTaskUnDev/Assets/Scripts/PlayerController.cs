using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Animator charAnim;
    private Rigidbody playerRb;
    private Vector3 playerVelocity;
    private PlayerInput playerInput;
    private GameObject vehicle;
    private VehicleController vehicleController;
    public ParticleSystem sparklesLeft;
    public ParticleSystem sparklesRight;
    private GameManager gameManager;
    private bool groundedPlayer;
    public bool isCollided; 
    private float jumpForce = 10;
    private float playerSpeed = 5.0f;
    private float rotationSpeed = 1.0f;
    private float gravity = -9.8f;
    private float jumpHeight = 1.0f;
    private Vector3 startPos;
    private Quaternion startRot;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();  
        playerInput = GetComponent<PlayerInput>(); 
        playerRb = GetComponent<Rigidbody>();
        charAnim = GetComponent<Animator>();
        vehicle = GameObject.Find("Vehicle");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        vehicleController = GameObject.Find("Vehicle").GetComponent<VehicleController>();
        startPos = transform.position;
        startRot = transform.rotation;
        //transform.position = vehicleController.PlayerSpawnPos();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.y < -3f)
        {
            transform.position = startPos;
            transform.rotation = startRot;
            gameManager.isActive = false;
        }

        if (!vehicleController.inVehicle)
        {
            Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector2 lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
            Vector3 rotation = new Vector3(0f, lookInput.x, 0f);
            transform.localEulerAngles += rotation * rotationSpeed;

            if (moveInput != Vector2.zero)
            {
                transform.position = move * playerSpeed;
                sparklesLeft.Play();
                sparklesRight.Play();
                charAnim.SetBool("isStatic", false);
            }
            else
            {
                charAnim.SetBool("isStatic", true);
                sparklesLeft.Stop();
                sparklesRight.Stop();
            }

            charAnim.SetFloat("ForwardMovement", moveInput.y);
            charAnim.SetFloat("SideMovement", moveInput.x);
            charAnim.SetFloat("fSpeed", Mathf.Sqrt((moveInput.x * moveInput.x) + (moveInput.y * moveInput.y)));
            if (moveInput.x == 0f) { charAnim.SetBool("bForward", false); }
            else { charAnim.SetBool("bForward", true); }
            if (moveInput.y == 0f) { charAnim.SetBool("bSide", false); }
            else { charAnim.SetBool("bSide", true); }

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            groundedPlayer = controller.isGrounded;
            charAnim.SetBool("isGrounded", groundedPlayer);
            if (playerInput.actions["Jump"].triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                charAnim.SetTrigger("Jump_trig");
                charAnim.SetBool("isGrounded", groundedPlayer);
            }
            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (playerInput.actions["Action"].triggered)
            {
                onAction();
            }
        }

    }

    private void onAction()
    {
        float dist = Mathf.Sqrt((vehicle.transform.position.x - transform.position.x) * 
            (vehicle.transform.position.x - transform.position.x) +
            (vehicle.transform.position.z - transform.position.z) * 
            (vehicle.transform.position.z - transform.position.z));
        if (dist <= 2f)
        {
            vehicleController.inVehicle = true;
            gameObject.SetActive(false);
        }  
    }

}
