using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstantiationData", menuName = "Data/Instantiation/New Instantiation Data")]
public class InstantiationData : ScriptableObject
{
    public GameObject prefab;
    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
}
