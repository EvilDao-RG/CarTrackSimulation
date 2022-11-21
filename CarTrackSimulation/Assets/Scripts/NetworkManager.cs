using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

[Serializable]
public class RequestWithArgs : UnityEvent<CarList>{}

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager Instance{
        get;
        private set;
    }

    public CarList cars;
    public string backendURL = "http://127.0.0.1:5000/";
    public RequestWithArgs requestWithArgs;
    private IEnumerator enumerator;


    private void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        enumerator = UpdatePositions(CarPoolManager.Instance._poolSize);
        Coroutine coroutine = StartCoroutine(enumerator);
    }

    void Update()
    {
        
    }

    IEnumerator UpdatePositions(int poolSize){
        while(true){
            string url = backendURL + "?size=" + poolSize;
            print(url); 
            UnityWebRequest request = UnityWebRequest.Get(backendURL + "?size=" + poolSize);
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success){
                Debug.LogError("NEL");
                print(request.result);
            } else {
                print(request.downloadHandler.text);
                cars = JsonUtility.FromJson<CarList>(request.downloadHandler.text);
                print(cars);
                requestWithArgs?.Invoke(cars);
            }    
            yield return new WaitForSeconds(1);
        }
    }
}
