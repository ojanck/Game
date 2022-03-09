using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Aeroplane;

public class EnterVehicleAirplane : MonoBehaviour
{
    private bool inVehicle = false;
    AeroplaneUserControl2Axis vehicleScript;
    public GameObject guiObj;
    GameObject player;
    public GameObject camera;

    void Start()
    {
        vehicleScript = GetComponent<AeroplaneUserControl2Axis>();
        player = GameObject.FindWithTag("Player");
        guiObj.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            guiObj.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                camera.SetActive(true);
                guiObj.SetActive(false);
                player.transform.parent = gameObject.transform;
                vehicleScript.enabled = true;
                player.SetActive(false);
                inVehicle = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(false);
        }
    }
    void Update()
    {
        if (inVehicle == true && Input.GetKey(KeyCode.F))
        {
            vehicleScript.enabled = false;
            camera.SetActive(false);
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
        }
    }
}