using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : MonoBehaviour
{
    [SerializeField]
    private Material[] _trafficLightsMaterials;
    private Material greenMaterial;
    private Material redMaterial;
    private Material currentMaterial;
    // Start is called before the first frame update
    void Start()
    {
        redMaterial = _trafficLightsMaterials[0];
        greenMaterial = _trafficLightsMaterials[1];
        currentMaterial = redMaterial;
        gameObject.GetComponent<Renderer>().material = currentMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            changeTrafficLightColor();
        }
    }

    void changeTrafficLightColor()
    {
        if (currentMaterial == greenMaterial){
            gameObject.GetComponent<Renderer>().material = redMaterial;
            currentMaterial = redMaterial;
        } else if (currentMaterial == redMaterial){
            gameObject.GetComponent<Renderer>().material = greenMaterial;
            currentMaterial = greenMaterial;
        }
    }
        
}
