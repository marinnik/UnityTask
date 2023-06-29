using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

    public GameObject player;
    public GameObject vehicle;
    private VehicleController vehicleController;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        vehicleController = GameObject.Find("Vehicle").GetComponent<VehicleController>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!vehicleController.inVehicle)
        {
            transform.position = player.transform.position + startPos;
        }
        else
        {
            transform.position = new Vector3(vehicle.transform.position.x + 4.5f, 
                vehicle.transform.position.y + 25f, 
                vehicle.transform.position.z + 0f);
        }
    }
}
