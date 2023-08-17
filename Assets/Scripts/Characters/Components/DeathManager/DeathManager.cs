using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathManager : IDeathManager
{
    private MonoBehaviour owner;

    public bool IsAlive { get; private set; } = true;

    public UnityEvent<MonoBehaviour> DeathEvent { get; } = new();

    public DeathManager(MonoBehaviour owner)
    {
        this.owner = owner;
    }

    public void Die()
    {
        if (IsAlive == false)
        {
            return;
        }

        IsAlive = false;

        DeathEvent.Invoke(owner);
    }
}
