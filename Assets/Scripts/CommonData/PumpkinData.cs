using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PumpkinData", menuName = "Data/Enemy Data/New Pumpkin Data")]
public class PumpkinData : ScriptableObject
{
    public GameObject pumpkinPrefab;
    public ePumpkinType pumpkinType = ePumpkinType.None;
    public string skinName = "orange";
    [Min(0f)] public float speed = 1f;
    [Min(0f)] public float size = 1f;
    public Color explosionColor;
    public Color dropletsColor;
}
