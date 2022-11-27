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
        
        Camera cam = gameObject.transform.GetChild(0).GetComponent<Camera>();
        
        cam.transform.position = new Vector3(transform.position.x, 
                                            transform.position.y + 2f, 
                                            transform.position.z);

        cam.transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90,
                                                transform.eulerAngles.y + 180,
                                                transform.eulerAngles.z + 90);
        cam.enabled = true;        
        CameraManager.Instance.DeactivateAll();

    }
}
