using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects/Car", order = 1)]

public class CarSO : ScriptableObject
{
    public int speed;
    public float scale;
    public GameObject prefab;
}
