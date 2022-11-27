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
        }
    }

    public void placeCars(CarList carList){
        for (int i = 0; i < cars.Length; i++){
            if (carsGO[i].transform.position.x < carList.cars[i].x){
                carsGO[i].transform.rotation = Quaternion.Euler(0, 90, 0);
            } else if (carsGO[i].transform.position.x > carList.cars[i].x){
                carsGO[i].transform.rotation = Quaternion.Euler(0, -90, 0);
            } else if (carsGO[i].transform.position.z < carList.cars[i].z) {
                carsGO[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            } else {
                carsGO[i].transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public void listenWithArgs(CarList cars){
        placeCars(cars);
    }
}
