using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CarDataManager : MonoBehaviour
{
    [SerializeField] private Car[] cars;
    private GameObject[] carsGO;
    [SerializeField] private CarSO[] carsSO;
    [SerializeField] private float speed;
    private Vector3[] targetPositions;

    private TrafficLight[] trafficLightStates;

    private IEnumerator enumerator;

    [SerializeField] private GameObject[] trafficLights;
    [SerializeField] private Material[] _trafficLightsMaterials;
    private Material greenMaterial;
    private Material redMaterial;
    private Material currentMaterial;

    public static CarDataManager Instance{
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
        carsGO = new GameObject[cars.Length];
        for (int i = 0; i < cars.Length; i++){
            carsGO[i] = CarPoolManager.Instance.Activate(Vector3.zero);
            carsGO[i].GetComponent<CarBuilder>().UpdateCar(carsSO[UnityEngine.Random.Range(0, carsSO.Length -1)]);
        }

        redMaterial = _trafficLightsMaterials[0];
        greenMaterial = _trafficLightsMaterials[1];

        for (int i = 0; i < trafficLights.Length; i++){
            
            trafficLights[i].gameObject.GetComponent<Renderer>().material = redMaterial;
        }
        
    }

    private void Update() {
        if(targetPositions != null){
            for(int i = 0; i < targetPositions.Length; i++){
                if(carsGO[i] != null && targetPositions[i] != Vector3.zero){
                    carsGO[i].transform.forward = targetPositions[i].normalized;
                    carsGO[i].transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
            }
        }

        if (trafficLightStates != null) {
            for (int i = 0; i < trafficLightStates.Length; i++){
                if (trafficLightStates[i].state == 5){
                    // print("Semaforo " + j + " Cambia a rojo");
                    changeTrafficLightColor(trafficLights[i], redMaterial);
                } else if (trafficLightStates[i].state == 7){
                    // print("Semaforo " + j + " Cambia a verde");
                    changeTrafficLightColor(trafficLights[i], greenMaterial);
                }
            }
        }        
    }

    public void placeCars(CarList carList){
        for (int i = 0; i < cars.Length; i++){
            if(carsGO[i] != null){
                Vector3 target = new Vector3(carList.cars[i].x, 0, carList.cars[i].z);
                carsGO[i].transform.position = target;
            }
        }
    }

    public void oneTimeListener(StepList track){
        targetPositions = new Vector3[cars.Length];
        trafficLightStates = new TrafficLight[trafficLights.Length];
        
        for(int i = 0; i < targetPositions.Length; i++){
            targetPositions[i] = new Vector3();
        }
        StartCoroutine(simulatorSteps(track));
    }



    IEnumerator simulatorSteps(StepList track){

        for(int i = 0; i < track.steps.Length; i++){
            CarList cars = new CarList(track.steps[i].cars);
            placeCars(cars);
            // Update traffic lights
            for (int k = 0; k < trafficLightStates.Length; k++){
                trafficLightStates[k] = track.steps[i].stop_lights[k];
            }
            // Update car positions
            for(int j = 0; j < targetPositions.Length; j++){
                if(i < track.steps.Length - 1){
                    if(track.steps[i + 1].cars[j].x == 0 && track.steps[i + 1].cars[j].z == 0){
                        if(carsGO[j] != null){
                            Destroy(carsGO[j]);
                        }
                    } else{
                        targetPositions[j] = new Vector3(
                            track.steps[i + 1].cars[j].x - track.steps[i].cars[j].x,
                            0,
                            track.steps[i + 1].cars[j].z - track.steps[i].cars[j].z 
                    );
                    }
                    
                } else {
                    targetPositions[j] = Vector3.forward;
                }
            }
            yield return new WaitForSeconds(1/speed);

        }

    }

    void changeTrafficLightColor(GameObject trafficLight, Material material)
    {
        trafficLight.gameObject.GetComponent<Renderer>().material = material;
    }

}


