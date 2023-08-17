using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Data/Enemy Spawn/New Enemy Spawn Data")]
public class EnemySpawnData : ScriptableObject
{
    public PumpkinData[] pumpkins;
    public float minSpawnTime;
    public float maxSpawnTime;
}
