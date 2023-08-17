using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonDefeateManager : IDefeateManager
{
    public bool IsLoser { get; private set; }

    public UnityEvent LoseEvent { get; } = new();

    public void Lose()
    {
        if (IsLoser)
        {
            return;
        }

        IsLoser = true;

        LoseEvent.Invoke();
    }
}
