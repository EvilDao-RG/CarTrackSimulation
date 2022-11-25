using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

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
        if(Input.GetKeyDown(KeyCode.Space)){
            NextCamera();
        }
    }

    public void NextCamera(){
        _cameras[_currentCamera].gameObject.SetActive(false);
        _currentCamera++;
        _currentCamera %= _cameras.Length;
        _cameras[_currentCamera].gameObject.SetActive(true);
    }
}
