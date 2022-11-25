using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //Detect when car is clicked and switch to car camera view
        Camera cam = gameObject.GetComponent<Camera>();
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
        cam.enabled = true;
        CameraManager.Instance.DeactivateAll();

    }
}
