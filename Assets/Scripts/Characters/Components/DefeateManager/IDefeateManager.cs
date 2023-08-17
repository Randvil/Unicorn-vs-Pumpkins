using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDefeateManager
{
    public void Lose();
    public bool IsLoser { get; }
    public UnityEvent LoseEvent { get; }
}
