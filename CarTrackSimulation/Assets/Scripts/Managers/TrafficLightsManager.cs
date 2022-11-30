using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : MonoBehaviour
{

    [SerializeField] private GameObject[] trafficLights;
    [SerializeField] private Material[] _trafficLightsMaterials;
    private Material greenMaterial;
    private Material redMaterial;
    private Material currentMaterial;


    public static TrafficLightsManager Instance{
        get;
        private set;
    }

    void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


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
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     changeTrafficLightColor();
        // }
    }

    void changeTrafficLightColor(GameObject trafficLight)
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
