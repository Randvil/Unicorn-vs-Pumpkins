using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDeathManager
{
    public void Die();
    public bool IsAlive { get; }
    public UnityEvent<MonoBehaviour> DeathEvent { get; }
}
