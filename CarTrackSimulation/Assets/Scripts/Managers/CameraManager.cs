using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    

    public static CameraManager Instance{
        get;
        private set;
    }

    void Awake()
    {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] private Camera[] _cameras;
    [SerializeField] private int _currentCamera;
    void Start()
    {
        for(int i = 0; i < _cameras.Length; i++){

            if(i == _currentCamera){
                _cameras[i].gameObject.SetActive(true);
            } else {
                _cameras[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     DeactivateAll();
        // }
    }

    public void NextCamera(){
        _cameras[_currentCamera].gameObject.SetActive(false);
        _currentCamera++;
        _currentCamera %= _cameras.Length;
        _cameras[_currentCamera].gameObject.SetActive(true);
    }

    public void DeactivateAll(){
        for(int i = 0; i < _cameras.Length; i++){
            
            _cameras[i].gameObject.SetActive(false);
            
        }
    }

    void OnMouseDown()
    {
        print("'Click'");
    }

}
