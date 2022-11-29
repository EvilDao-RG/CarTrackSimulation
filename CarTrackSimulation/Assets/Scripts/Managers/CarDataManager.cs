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
    private Vector3[] targetPositions;
    private IEnumerator enumerator;

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
    }

    private void Update() {
        if(targetPositions != null){
            for(int i = 0; i < targetPositions.Length; i++){
                if(targetPositions[i] != Vector3.zero){
                    carsGO[i].transform.Translate(Vector3.forward * Time.deltaTime);
                    carsGO[i].transform.forward = targetPositions[i].normalized;
                }
            }
        }
    }

    public void placeCars(CarList carList){
        for (int i = 0; i < cars.Length; i++){
            Vector3 target = new Vector3(carList.cars[i].x, 0, carList.cars[i].z);
            carsGO[i].transform.position = target;
        }
    }

    public void oneTimeListener(StepList track){
        targetPositions = new Vector3[cars.Length];
        for(int i = 0; i < targetPositions.Length; i++){
            targetPositions[i] = new Vector3();
        }
        StartCoroutine(simulatorSteps(track));
    }

    IEnumerator simulatorSteps(StepList track){
        for(int i = 0; i < track.steps.Length; i++){
            CarList cars = new CarList(track.steps[i].cars);
            placeCars(cars);
            for(int j = 0; j < targetPositions.Length; j++){    
                if(i < track.steps.Length - 1){
                    targetPositions[j] = new Vector3(
                        track.steps[i + 1].cars[j].x - track.steps[i].cars[j].x,
                        0,
                        track.steps[i + 1].cars[j].z - track.steps[i].cars[j].z 
                    );
                } else {
                    targetPositions[j] = Vector3.zero;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}
