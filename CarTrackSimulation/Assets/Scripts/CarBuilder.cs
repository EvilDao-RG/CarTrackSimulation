using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuilder : MonoBehaviour
{
    [SerializeField] CarSO _data;
    public GameObject _innerCar;
    

    private void Awake() {
        // Build car using data
        _innerCar = Instantiate<GameObject>(_data.prefab, transform.position, transform.rotation, transform);
        _innerCar.transform.localScale = new Vector3(_data.scale, _data.scale, _data.scale);
    }

    void Start() {
        
    }

    private void UpdateCar(){

        if(_innerCar != null){
            Destroy(_innerCar);
        }
        _innerCar = Instantiate<GameObject>(_data.prefab, transform.position, transform.rotation, transform);
        _innerCar.transform.localScale = new Vector3(_data.scale, _data.scale, _data.scale);

    }

    public void UpdateCar(CarSO newCar){
        _data = newCar;
        UpdateCar();
    }

}
