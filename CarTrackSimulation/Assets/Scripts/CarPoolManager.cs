using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPoolManager : MonoBehaviour
{
    
    public static CarPoolManager Instance{
        get;
        private set;
    }

    [SerializeField] private GameObject car;
    [SerializeField] public int _poolSize;
    private Queue<GameObject> _pool;

    private void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _pool = new Queue<GameObject>();

        for(int i = 0; i < _poolSize; i++){
            GameObject temp = Instantiate<GameObject>(car);
            temp.SetActive(false);
            _pool.Enqueue(temp);
        }
    }

    public GameObject Activate(Vector3 position){
        if(_pool.Count == 0){
            return null;
        }

        GameObject current = _pool.Dequeue();
        current.SetActive(true);
        current.transform.position = position;

        return current;
    }

    public void Deactivate(GameObject objectToDestroy){
        objectToDestroy.SetActive(false);
        _pool.Enqueue(objectToDestroy);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
