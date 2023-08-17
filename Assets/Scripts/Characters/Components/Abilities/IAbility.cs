using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAbility
{
    public void Activate();
    public void Deactivate();
    public bool IsActivated { get; }
    public UnityEvent ActivationEvent { get; }
    public UnityEvent DeactivationEvent { get; }
}
