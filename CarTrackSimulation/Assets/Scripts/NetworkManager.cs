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


    private void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void UpdatePositions(int poolSize){
        bool complete = true;
        while(complete){
            string url = backendURL + "?size=" + poolSize;
            print(url); 
            UnityWebRequest request = UnityWebRequest.Get(backendURL + "?size=" + poolSize);
            request.SendWebRequest();
            new WaitForSeconds(2);

            if(request.result != UnityWebRequest.Result.Success){
                Debug.LogError("NEL");
                print(request.result);
            } else {
                print(request.downloadHandler.text);
                cars = JsonUtility.FromJson<CarList>(request.downloadHandler.text);
                requestWithArgs?.Invoke(cars);
            }    
            complete = false;
        }
    }
}
