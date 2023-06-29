using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCortroller : MonoBehaviour
{

    private PlayerInput playerInput;
    public GameObject player;
    public GameObject vehicle;
    private VehicleController vehicleController;
    private float rotationSpeed = 1.0f;
    private float distP;
    private float distV;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        GameObject player = GameObject.Find("Player");
        vehicleController = GameObject.Find("Vehicle").GetComponent<VehicleController>();
        distP = Mathf.Sqrt(8);
        distV = 4;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 input = playerInput.actions["Look"].ReadValue<Vector2>();
        Vector3 rotation = new Vector3(input.y, input.x, 0f);
        transform.localEulerAngles += rotation * rotationSpeed;
        if (!vehicleController.inVehicle)
        {
            transform.position = player.transform.position - transform.forward * distP + transform.up;
        }
        else 
        {
            transform.position = vehicle.transform.position - transform.forward * distV + transform.up; 
        }
        
    }
}
